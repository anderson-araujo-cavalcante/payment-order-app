using Microsoft.AspNetCore.Http;

namespace PaymentOrderWeb.Application.Interfaces
{
    public interface IPaymentOrderAppService
    {
        Task ProcessAsync(IEnumerable<IFormFile> files);
    }
}
