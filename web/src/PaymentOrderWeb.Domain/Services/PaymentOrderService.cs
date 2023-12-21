using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.Domain.Extensions;
using PaymentOrderWeb.Domain.Interfaces.Services;
using PaymentOrderWeb.Infrasctructure.Extensions;
using System.Collections.Concurrent;

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

                var fileName = file.Key[..file.Key.IndexOf('-')];
                var monthReference = file.Value.First().Date.Month;
                var yearReference = file.Value.First().Date.Year;

                var month = 0;

                if (workingDaysPerMonth.TryGetValue(monthReference, out var value))
                {
                    month = value;
                }
                else
                {
                    var totalBusinessDays = new DateTime(yearReference, monthReference, 1).TotalBusinessDaysInMonth();
                    workingDaysPerMonth.Add(monthReference, totalBusinessDays);
                    month = totalBusinessDays;
                }

                var groupByEmployee = file.Value.GroupBy(x => x.Code);

                if (SynchronizationContext.Current == null)
                    SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

                await groupByEmployee.AsyncParallelForEach(async employeeMonth =>
                {
                    var data = employeeMonth.First();
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
                        //HourlyRate = employeeMonth.First().HourlyRate
                    };

                    department.Employees.Add(employee);

                }, 20, TaskScheduler.FromCurrentSynchronizationContext());

                department.Name = fileName;
                department.ReferenceMonth = monthReference.ToString();
                department.ReferenceYear = yearReference.ToString();
                department.TotalDiscount = department.Employees.Sum(x => (x.DebitHours + (x.MissingDays * DAILY_WORKLOAD)) * x.HourlyRate);
                department.TotalExtra = department.Employees.Sum(x => (x.ExtraHours + (x.ExtraDays * DAILY_WORKLOAD)) * x.HourlyRate);
                department.TotalToPay = department.Employees.Sum(x => x.TotalReceivable);

                departmentsFinal.Add(department);

            }, 20, TaskScheduler.FromCurrentSynchronizationContext());

            return departmentsFinal;
        }
    }
}
