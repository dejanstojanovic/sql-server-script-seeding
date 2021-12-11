using System.Reflection;

namespace EntityFrameworkCore.SqlServer.Seeding.Models
{
    /// <summary>
    /// Seeding options model
    /// </summary>
    public class SeedingOptions
    {
        readonly string _scriptsFolder;
        readonly Assembly _scriptsAssembly;

        /// <summary>
        /// Scripts folder inside scripts assembly
        /// </summary>
        public string ScriptsFolder { get => _scriptsFolder; }

        /// <summary>
        /// Assembly containing scripts
        /// </summary>
        public Assembly ScriptsAssembly { get => _scriptsAssembly; }

        public SeedingOptions(string scriptsFolder, Assembly scriptsAssembly)
        {
            _scriptsFolder = scriptsFolder;
            _scriptsFolder = scriptsFolder;
        }
    }
}
