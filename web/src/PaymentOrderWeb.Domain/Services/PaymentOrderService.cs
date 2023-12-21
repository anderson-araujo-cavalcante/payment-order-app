using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.Domain.Extensions;
using PaymentOrderWeb.Domain.Interfaces.Services;
using PaymentOrderWeb.Infrasctructure.Extensions;
using System.Collections.Concurrent;

namespace PaymentOrderWeb.Domain.Services
{
    public class PaymentOrderService : IPaymentOrderService
    {
        public async Task Process(IEnumerable<EmployeeData> employees)
        {
            Parallel.ForEach(employees, employee => { });
        }

        public async Task<IEnumerable<Department>> Process1Async(IDictionary<string, IEnumerable<EmployeeData>> employees)
        {
            List<Department> departmentsFinal = new List<Department>();
            IDictionary<int, int> workingDaysPerMonth = new Dictionary<int, int>();

            if (SynchronizationContext.Current == null)
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            var exceptions = new ConcurrentQueue<Exception>();

            await employees.AsyncParallelForEach(async file =>
            {
                IList<Department> departments = new List<Department>();
                var department = new Department
                {
                    Employees = new List<Employee>()
                };
                var fileName = file.Key[..file.Key.IndexOf('-')];
                department.Name = fileName;
                department.ReferenceMonth = file.Value.First().Date.Month.ToString();
                department.ReferenceYear = file.Value.First().Date.Year.ToString();

                var month = 0;

                if (workingDaysPerMonth.TryGetValue(4, out var value))
                {
                    month = value;
                }
                else
                {
                    //pegar dias uteis
                    var diasUteis = 21;
                    workingDaysPerMonth.Add(4, diasUteis);
                    month = diasUteis;
                }

                //agrupar por pessoa
                var groupByEmployee = file.Value.GroupBy(x => x.Code);

                if (SynchronizationContext.Current == null)
                    SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

                await groupByEmployee.AsyncParallelForEach(async employeeMonth =>
                {
                    var employee = new Employee
                    {
                        Name = employeeMonth.First().Name,
                        Code = employeeMonth.Key,
                        TotalReceivable = employeeMonth.Sum(x => x.TotalDay()),
                        ExtraHours = employeeMonth.TimeSpanSum(x => x.TotalExtraDay()).TotalHours,
                        DebitHours = employeeMonth.TimeSpanSum(x => x.TotalDiscountDay()).TotalHours,
                        MissingDays = month - employeeMonth.Count(x => x.IsBusinessDay()),
                        ExtraDays = employeeMonth.Count(x => !x.IsBusinessDay()),
                        WorkedDays = employeeMonth.Count()
                    };

                    department.Employees.Add(employee);

                }, 20, TaskScheduler.FromCurrentSynchronizationContext());

                department.TotalDiscount = department.Employees.Sum(x => (x.DebitHours + (x.MissingDays * 8)) * x.HourlyRate);
                department.TotalExtra = department.Employees.Sum(x => (x.ExtraHours + (x.ExtraDays * 8)) * x.HourlyRate);
                department.TotalToPay = department.Employees.Sum(x => x.TotalReceivable);

                departmentsFinal.Add(department);

            }, 20, TaskScheduler.FromCurrentSynchronizationContext());

            return departmentsFinal;
        }
    }
}
