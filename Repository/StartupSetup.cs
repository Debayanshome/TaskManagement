using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TaskManagement.Shared.Repository.Interface;
using TaskManagement.Repository.Context;

namespace TaskManagement.Repository
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
            services.AddDbContextFactory<TaskDbContext>(opt => opt.UseMySql(configuration.GetConnectionString("DefaultConnection")!, serverVersion, b => b.MigrationsAssembly("TaskManagement.Api")), ServiceLifetime.Scoped);

            AddDependencies(services);
        }

        private static void AddDependencies(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        }
    }
}
