using EntityFrameworkCore.SqlServer.Seeding.Configurations;
using EntityFrameworkCore.SqlServer.Seeding.Entities;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.SqlServer.Seeding;

/// <inheritdoc />
public class SeedingDbContext : DbContext
{
	/// <inheritdoc />
	public SeedingDbContext() : base() { }

	/// <inheritdoc />
	public SeedingDbContext(DbContextOptions<SeedingDbContext> options) : base(options) { }

	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new SeedingEntryConfiguration());
	}

	internal virtual DbSet<SeedingEntry> SeedingEntries { get; set; }
}