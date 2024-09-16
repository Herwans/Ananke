using Ananke.Infrastructure.Repository;
using Ananke.Infrastructure.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ananke.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IExtensionRepository, ExtensionRepository>();
            services.AddDbContext<AnankeContext>(options =>
                options.UseNpgsql("Host=localhost;Database=ananke;Username=postgres;Password=admin",
                x => x.MigrationsAssembly("Ananke.Infrastructure")));
            return services;
        }
    }
}