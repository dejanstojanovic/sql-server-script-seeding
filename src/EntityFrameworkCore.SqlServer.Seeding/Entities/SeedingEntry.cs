namespace EntityFrameworkCore.SqlServer.Seeding.Entities;

/// <summary>
/// Seeding history entry
/// </summary>
internal class SeedingEntry
{
	/// <summary>
	/// Name of the script executed
	/// </summary>
	public string Name { get; set; } = string.Empty;
}