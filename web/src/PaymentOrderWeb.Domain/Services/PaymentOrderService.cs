using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.Domain.Interfaces.Services;
using PaymentOrderWeb.Infrasctructure.Extensions;

namespace PaymentOrderWeb.Domain.Services
{
    public class PaymentOrderService : IPaymentOrderService
    {
        public async Task Process(IEnumerable<Employee> employees)
        {
            Parallel.ForEach(employees, employee => { });
        }

        public async Task Process1(IDictionary<string, IEnumerable<EmployeeData>> employees)
        {


            //retorno dados finais
            IEnumerable<Department> departments;


            //agrupar por mês/ano


            await employees.AsyncParallelForEach(async file =>
            {
                //agrupar por pessoa

                //validar

                //calcular dados do funcionario

                //pegar dados do departamento através do nome

                //calcular dados do departamento


            }, 20, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
