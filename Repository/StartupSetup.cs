using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Repository
{
    public class StartupSetup
    {
      /*  public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbUserContext, DbUserContext>();

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
            services.AddDbContextFactory<IdentityDbContext>(opt => opt.UseMySql(configuration.GetConnectionString("DefaultConnection")!, serverVersion, b => b.MigrationsAssembly("DT.Identity.Api")), ServiceLifetime.Scoped);

            AddDependencies(services);
        }

        private static void AddDependencies(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        }*/
    }
}
