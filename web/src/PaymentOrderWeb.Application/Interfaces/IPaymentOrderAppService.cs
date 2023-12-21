using Microsoft.AspNetCore.Http;
using PaymentOrderWeb.Domain.Entities;

namespace PaymentOrderWeb.Application.Interfaces
{
    public interface IPaymentOrderAppService
    {
        Task<IEnumerable<Department>> ProcessAsync(IEnumerable<IFormFile> files);
    }
}
