using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.Domain.Interfaces.Services;

namespace PaymentOrderWeb.Domain.Services
{
    public class PaymentOrderService : IPaymentOrderService
    {
        public async Task Process(IEnumerable<Employee> employees)
        {
            Parallel.ForEach(employees, employee => { });
        }
    }
}
