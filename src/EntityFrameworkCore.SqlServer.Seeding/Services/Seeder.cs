using EntityFrameworkCore.SqlServer.Seeding.Extensions;
using EntityFrameworkCore.SqlServer.Seeding.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EntityFrameworkCore.SqlServer.Seeding.Services
{
    /// <inheritdoc/>
    public class Seeder : ISeeder
    {
        readonly SeedingDbContext _context;
        readonly SeedingOptions _seedingOptions;
        readonly string _filePrefix;
        public Seeder(SeedingDbContext context, SeedingOptions options)
        {
            _context = context;
            _seedingOptions = options;
            _filePrefix = $"{_seedingOptions.ScriptsAssembly.GetName().Name}";
            if (!string.IsNullOrWhiteSpace(_seedingOptions.ScriptsFolder))
                _filePrefix = $"{_filePrefix}.{_seedingOptions.ScriptsFolder}.";

            context.Database.Migrate();
        }

        /// <inheritdoc/>
        public void ExecutePendingScripts()
        {
            var pendingScripts = GetPendingScripts();
            if(pendingScripts!= null && pendingScripts.Any())
                foreach(var script in pendingScripts)
                    ExecuteScript(script);
        }

        /// <inheritdoc/>
        public void ExecuteScript(string fileName)
        {
            var command = GetScript(fileName);
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlRaw(command);
                    _context.SeedingEntries.Add(new Entities.SeedingEntry() { Name = fileName });
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetAllScripts()
        {
            var scriptFiles = _seedingOptions.ScriptsAssembly.GetManifestResourceNames()
                                .Where(f => f.StartsWith(_filePrefix) && f.EndsWith(".sql"))
                                                              .Select(f => f.Replace(_filePrefix, String.Empty))
                                                              .OrderBy(f => f)
                                                              .ToArray();

            return scriptFiles;
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetExecutedScripts()
        {
            if (_context.DatabaseExists())
                return _context.SeedingEntries.AsNoTracking().Select(e => e.Name).ToArray().OrderBy(n => n);
            else
                return new string[] { };
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetPendingScripts()
        {
            var scriptFiles = GetAllScripts();
            var executedScripts = GetExecutedScripts();

            var pending = scriptFiles.Where(f => !executedScripts.Contains(f));
            return pending;

        }

        /// <inheritdoc/>
        public string GetScript(string fileName)
        {
            if (!fileName.StartsWith(_filePrefix))
                fileName = string.Concat(_filePrefix, fileName);

            using (Stream stream = _seedingOptions.ScriptsAssembly.GetManifestResourceStream(fileName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
