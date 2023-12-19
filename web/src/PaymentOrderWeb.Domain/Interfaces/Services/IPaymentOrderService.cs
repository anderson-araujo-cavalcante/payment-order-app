using PaymentOrderWeb.Domain.Entities;

namespace PaymentOrderWeb.Domain.Interfaces.Services
{
    public interface IPaymentOrderService
    {
        Task Process(IEnumerable<Employee> employees);
        Task Process1(IDictionary<string, IEnumerable<EmployeeData>> employees);
    }
}
