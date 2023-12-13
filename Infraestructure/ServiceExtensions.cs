using Domain.RepositoryInterface;
using Infraestructure.Context;
using Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class ServiceExtensions
    {
        public static void AddServiceExtensionsInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Connection
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // Repositories Dependency Injection
            services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
        }
    }
}
