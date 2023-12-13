using Application.IServices;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddServiceExtensionsApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuracion de autommaper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Services Dependency Injection
            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
        }
    }
}
