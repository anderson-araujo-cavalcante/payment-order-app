using Microsoft.Extensions.DependencyInjection;
using PaymentOrderWeb.Application.Concrets;
using PaymentOrderWeb.Application.Interfaces;

namespace PaymentOrderWeb.Application.Configurations
{
    public static class AppModuleDependency
    {
        public static void AddAppModule(this IServiceCollection services)
        {
            services.AddScoped<IPaymentOrderAppService, PaymentOrderAppService>();
        }
    }
}
