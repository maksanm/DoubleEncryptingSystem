using Decoder.Infrastructure.Persistance;
using Decryptor.Core.Services;
using Decryptor.Core.Services.Interfaces;
using Decryptor.Core.Services.Interfaces.Decryptors;
using Decryptor.Infrastructure.Services.Decryptors;
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
            services.AddDecryptors();

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseContext(configuration);
            services.AddTransient<IDecryptorService, DoubleDecryptorService>();

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

        private static IServiceCollection AddDecryptors(this IServiceCollection services)
        {
            services.AddSingleton<IAsymmetricDecryptor, RSADecryptor>();
            services.AddSingleton<ISymmetricDecryptor, AESDecryptor>();

            return services;
        }
    }
}
