using Decoder.Application.Interfaces;
using Decoder.Infrastructure.Persistance;
using Decoder.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Decoder.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddApplicationServices(configuration);

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseContext(configuration);
            services.AddDecoderService();

            return services;
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);

            return services;
        }

        private static IServiceCollection AddDecoderService(this IServiceCollection services)
        {
            services.AddTransient<IDecryptorService, DecoderService>();

            return services;
        }
    }
}
