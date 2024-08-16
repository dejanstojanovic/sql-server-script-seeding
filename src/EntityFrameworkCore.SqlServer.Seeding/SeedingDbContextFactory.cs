using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkCore.SqlServer.Seeding;

/// <inheritdoc />
public class SeedingDbContextFactory : IDesignTimeDbContextFactory<SeedingDbContext>
{
	/// <inheritdoc />
	public SeedingDbContext CreateDbContext(string[] args)
	{
		var dbContext = new SeedingDbContext(new DbContextOptionsBuilder<SeedingDbContext>().UseSqlServer(
			new ConfigurationBuilder()
				.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.json"))
				.Build()
				.GetConnectionString("SeedingDatabase")
		).Options);
		dbContext.Database.Migrate();
		return dbContext;
	}
}