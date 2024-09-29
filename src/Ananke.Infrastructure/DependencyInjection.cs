using Ananke.Infrastructure.Persistence.EFCore;
using Ananke.Infrastructure.Persistence.EFCore.Repositories;
using Ananke.Infrastructure.Persistence.EFCore.Repositories.Media.Images;
using Ananke.Infrastructure.Persistence.Interfaces.Repositories;
using Ananke.Infrastructure.Persistence.Interfaces.Repositories.Media.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ananke.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IExtensionRepository, ExtensionRepository>();
            services.AddScoped<IGalleryRepository, GalleryRepository>();
            services.AddDbContext<AnankeContext>(options =>
                options.UseNpgsql(connectionString,
                x => x.MigrationsAssembly("Ananke.Infrastructure")));
            return services;
        }
    }
}