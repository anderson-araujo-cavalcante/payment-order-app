using PaymentOrderWeb.Domain.Entities;

namespace PaymentOrderWeb.Domain.Interfaces.Services
{
    public interface IPaymentOrderService
    {
        Task<IEnumerable<Department>> Process1Async(IDictionary<string, IEnumerable<EmployeeData>> employees);
    }
}
