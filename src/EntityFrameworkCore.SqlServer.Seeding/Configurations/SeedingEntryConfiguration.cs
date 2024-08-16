using EntityFrameworkCore.SqlServer.Seeding.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.SqlServer.Seeding.Configurations;

/// <summary>
/// Seeding table configuration
/// </summary>
internal class SeedingEntryConfiguration : IEntityTypeConfiguration<SeedingEntry>
{
	/// <summary>
	/// Configures data structure for seeding history table
	/// </summary>
	/// <param name="builder"></param>
	public void Configure(EntityTypeBuilder<SeedingEntry> builder)
	{
		builder.ToTable("__SeedingHistory");
		builder.HasKey(s => s.Name);
	}
}