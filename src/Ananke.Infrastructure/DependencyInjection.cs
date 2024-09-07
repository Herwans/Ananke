using Ananke.Infrastructure.Repository;
using Ananke.Infrastructure.Repository.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Ananke.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IExtensionRepository, ExtensionRepository>();
            return services;
        }
    }
}