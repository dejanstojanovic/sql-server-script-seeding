using System;

namespace EntityFrameworkCore.SqlServer.Seeding.Entities
{
    /// <summary>
    /// Seeding history entry
    /// </summary>
    class SeedingEntry
    {
        /// <summary>
        /// Name of the script executed
        /// </summary>
        public String Name { get; set; }
    }
}
