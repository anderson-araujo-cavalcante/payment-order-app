using PaymentOrderWeb.Domain.Entities;

namespace PaymentOrderWeb.Application.Interfaces
{
    public interface IPaymentOrderAppService
    {
        Task Process(IEnumerable<EmployeeData> employees);
    }
}
