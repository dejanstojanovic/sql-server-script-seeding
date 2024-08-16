using System.Reflection;

namespace EntityFrameworkCore.SqlServer.Seeding.Models;

/// <summary>
/// Seeding options model
/// </summary>
public class SeedingOptions(string scriptsFolder, Assembly scriptsAssembly)
{
	/// <summary>
	/// Scripts folder inside scripts assembly
	/// </summary>
	public string ScriptsFolder { get; } = scriptsFolder;

	/// <summary>
	/// Assembly containing scripts
	/// </summary>
	public Assembly ScriptsAssembly { get; } = scriptsAssembly;
}