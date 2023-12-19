using Microsoft.Extensions.DependencyInjection;
using PaymentOrderWeb.Domain.Interfaces.Services;
using PaymentOrderWeb.Domain.Services;

namespace PaymentOrderWeb.Domain.Configurations
{
    public static class DomainModuleDependency
    {
        public static void AddDomainModule(this IServiceCollection services)
        {
            services.AddScoped<IPaymentOrderService, PaymentOrderService>();
        }
    }
}
