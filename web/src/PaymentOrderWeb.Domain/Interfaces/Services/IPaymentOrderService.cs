using PaymentOrderWeb.Domain.Entities;

namespace PaymentOrderWeb.Domain.Interfaces.Services
{
    public interface IPaymentOrderService
    {
        Task<IEnumerable<Department>> ProcessAsync(IDictionary<string, IEnumerable<EmployeeData>> employees);
    }
}
