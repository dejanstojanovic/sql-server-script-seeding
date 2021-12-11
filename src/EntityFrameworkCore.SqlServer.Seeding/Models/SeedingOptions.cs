using System.Reflection;

namespace EntityFrameworkCore.SqlServer.Seeding.Models
{
    public class SeedingOptions
    {
        readonly string _scriptsFolder;
        readonly Assembly _scriptsAssembly;
        public string ScriptsFolder { get => _scriptsFolder; }
        public Assembly ScriptsAssembly { get => _scriptsAssembly; }

        public SeedingOptions(string scriptsFolder, Assembly scriptsAssembly)
        {
            _scriptsFolder = scriptsFolder;
            _scriptsFolder = scriptsFolder;
        }
    }
}
