using EntityFrameworkCore.SqlServer.Seeding.Constants;
using EntityFrameworkCore.SqlServer.Seeding.Models;
using EntityFrameworkCore.SqlServer.Seeding.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace EntityFrameworkCore.SqlServer.Seeding.Extensions
{
    /// <summary>
    /// SQL server script seeding extensions
    /// </summary>
    public static class SeedingExtensions
    {
        /// <summary>
        /// Checks if database defined in dbContext connection string exists
        /// </summary>
        /// <param name="context">Relation database context</param>
        /// <returns></returns>
        public static bool DatabaseExists(this DbContext context)
        {
            return (context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
        }

        /// <summary>
        /// Enables script seeding
        /// </summary>
        /// <param name="services">Services collection</param>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="scriptsFolder">Scripts folder inside scripts assembly</param>
        /// <param name="seedingAssembly">Assembly containing seeding scripts</param>
        public static void AddScriptSeeding(
            this IServiceCollection services,
            string connectionString,
            Assembly seedingAssembly,
            String scriptsFolder)
        {
            services.AddDbContext<SeedingDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    x =>
                    {
                        x.MigrationsAssembly(typeof(SeedingExtensions).Assembly.GetName().Name);
                    }
                );
            });

            services.AddSingleton<SeedingOptions>(o => new SeedingOptions(scriptsFolder, seedingAssembly));
            services.AddTransient<ISeeder, Seeder>();
        }

        /// <summary>
        /// Seeds scripts
        /// </summary>
        /// <param name="app">Application builder instance</param>
        public static void SeedFromScripts(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                var seeder = serviceScope.ServiceProvider.GetService<ISeeder>();
                seeder.ExecutePendingScripts();
            }
        }
    }
}
