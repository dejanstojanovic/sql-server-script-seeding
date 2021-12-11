using EntityFrameworkCore.SqlServer.Seeding.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Seeding.Data.Infrastructure.Constants;
using System;
using System.IO;
using System.Linq;

namespace Sample.Seeding.Data.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static void AddEmployeesData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(DbContextConfigConstants.DB_CONNECTION_CONFIG_NAME);
            services.AddDbContext<EmployeesDatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    x =>
                    {
                        x.MigrationsHistoryTable("__EFMigrationsHistory");
                        x.MigrationsAssembly(typeof(EmployeesDatabaseContext).Assembly.GetName().Name);
                    }
                );
            });

            services.AddScriptSeeding(connectionString, typeof(EmployeesDatabaseContext).Assembly, "Seedings");
        }

        public static void MigrateEmployeesData(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<EmployeesDatabaseContext>())
                {
                    context.Database.Migrate();
                }
            }
            app.SeedFromScripts();
        }
    }
}
