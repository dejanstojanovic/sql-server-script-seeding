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

        public static void MigrateAndSeedEmployeesData(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<EmployeesDatabaseContext>())
                {
                    var migrator = context.Database.GetService<IMigrator>();
                    var migrations = context.Database.GetPendingMigrations();
                    var seeder = serviceScope.ServiceProvider.GetService<ISeeder>();
                    var seeds = seeder.GetPendingScripts();

                    var commands = migrations.Concat(seeds).OrderBy(c => c).ToList();

                    if (commands != null && commands.Any())
                    {
                        foreach (var command in commands)
                        {
                            if (command.EndsWith(".sql"))
                                seeder.ExecuteScript(command);
                            else
                                migrator.Migrate(command);
                        }
                    }
                }
            }
        }
    }
}
