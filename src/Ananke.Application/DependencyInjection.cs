using Ananke.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ananke.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IFileSystemService, FileSystemService>();
            return services;
        }
    }
}