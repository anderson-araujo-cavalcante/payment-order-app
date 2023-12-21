using CsvHelper;
using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.Domain.Extensions;
using PaymentOrderWeb.Domain.Interfaces.Services;
using PaymentOrderWeb.Infrasctructure.Enums;
using PaymentOrderWeb.Infrasctructure.Exceptions;
using PaymentOrderWeb.Infrasctructure.Extensions;
using PaymentOrderWeb.Infrasctructure.Helpers;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PaymentOrderWeb.Domain.Services
{
    public class PaymentOrderService : IPaymentOrderService
    {
        private const int DAILY_WORKLOAD = 8;

        public async Task<IEnumerable<Department>> ProcessAsync(IDictionary<string, IEnumerable<EmployeeData>> employees)
        {
            List<Department> departmentsFinal = new List<Department>();
            IDictionary<int, int> workingDaysPerMonth = new Dictionary<int, int>();

            if (SynchronizationContext.Current == null)
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            var exceptions = new ConcurrentQueue<Exception>();

            await employees.AsyncParallelForEach(async file =>
            {
                IList<Department> departments = new List<Department>();
                var department = new Department();

                var dataFile = file.Key.Replace(".csv","").Split('-');
                var fileName = dataFile[0];
                var monthReference = (int)EnumHelper.ToMonthEnum(dataFile[1]);
                var yearReference = Convert.ToInt16(dataFile[2]);
                var dataReference = new DateTime(yearReference, monthReference, 1);

                var month = 0;

                if (workingDaysPerMonth.TryGetValue(monthReference, out var value))
                {
                    month = value;
                }
                else
                {
                    var totalBusinessDays = dataReference.TotalBusinessDaysInMonth();
                    workingDaysPerMonth.Add(monthReference, totalBusinessDays);
                    month = totalBusinessDays;
                }

                var groupByEmployee = file.Value.GroupBy(x => x.Code);

                if (SynchronizationContext.Current == null)
                    SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

                var exceptions = new ConcurrentQueue<Exception>();

                await groupByEmployee.AsyncParallelForEach(async employeeMonth =>
                {
                    try
                    {
                        var data = employeeMonth.First();

                        if (data.Date.Year != dataReference.Date.Year || data.Date.Month != dataReference.Date.Month) throw new InconsistentSpreadsheetException(fileName);

                        var employee = new Employee
                        {
                            Name = data.Name,
                            Code = employeeMonth.Key,
                            HourlyRate = employeeMonth.First().HourlyRate,
                            TotalReceivable = employeeMonth.Sum(x => x.TotalValueDay()),
                            ExtraHours = employeeMonth.TimeSpanSum(x => x.TotalTimeExtraDay()).TotalHours,
                            DebitHours = employeeMonth.TimeSpanSum(x => x.TotalTimeDiscountDay()).TotalHours,
                            MissingDays = month - employeeMonth.Count(x => x.Date.IsBusinessDay()),
                            ExtraDays = employeeMonth.Count(x => !x.Date.IsBusinessDay()),
                            WorkedDays = employeeMonth.Count()
                        };

                        department.Employees.Add(employee);
                    }
                    catch (Exception)
                    {
                        exceptions.Enqueue(new InconsistentSpreadsheetException(file.Key));
                    }

                }, 30, TaskScheduler.FromCurrentSynchronizationContext());

                if (!exceptions.IsEmpty)
                {
                    throw new AggregateException(exceptions);
                }

                department.Name = fileName;
                department.ReferenceMonth = ((MonthEnum)monthReference).GetEnumDescription();
                department.ReferenceYear = yearReference;
                department.TotalDiscount = department.Employees.Sum(x => (x.DebitHours + (x.MissingDays * DAILY_WORKLOAD)) * x.HourlyRate);
                department.TotalExtra = department.Employees.Sum(x => (x.ExtraHours + (x.ExtraDays * DAILY_WORKLOAD)) * x.HourlyRate);
                department.TotalToPay = department.Employees.Sum(x => x.TotalReceivable);

                departmentsFinal.Add(department);

            }, 20, TaskScheduler.FromCurrentSynchronizationContext());

            if (!exceptions.IsEmpty)
            {
                throw new AggregateException(exceptions);
            }

            return departmentsFinal;
        }
    }
}
