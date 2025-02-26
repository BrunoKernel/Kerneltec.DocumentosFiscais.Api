using Kerneltec.DocumentosFiscais.Infra.Data.Context;
using Kerneltec.DocumentosFiscais.Infra.Integrations.Acbr;
using Kerneltec.DocumentosFiscais.Infra.Integrations.Acbr.Configuration;
using Kerneltec.DocumentosFiscais.Infra.Integrations.Acbr.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kerneltec.DocumentosFiscais.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddOptions<AppConfig>()
            .Bind(configuration.GetSection("ACBrIni"))
            .ValidateOnStart();


            services.AddSingleton<IACBrNFePool, ACBrNFePool>();

            //services.AddScoped<IACBrNFeService, ACBrNFeService>();

            return services;
        }
    }
}
