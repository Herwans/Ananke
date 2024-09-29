using Ananke.Infrastructure.Repository;
using Ananke.Infrastructure.Repository.EFCore;
using Ananke.Infrastructure.Repository.EFCore.Media.Images;
using Ananke.Infrastructure.Repository.Media.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ananke.Application
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