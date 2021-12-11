using System.Collections.Generic;

namespace EntityFrameworkCore.SqlServer.Seeding.Services
{
    /// <summary>
    /// Seeding service
    /// </summary>
    public interface ISeeder
    {
        /// <summary>
        /// Retrieves all embedded script files
        /// </summary>
        /// <returns>All embedded script files</returns>
        IEnumerable<string> GetAllScripts();

        /// <summary>
        /// Retrieves executed seeding scripts
        /// </summary>
        /// <returns>Already executes scripts</returns>
        IEnumerable<string> GetExecutedScripts();

        /// <summary>
        /// Retrieves scripts that are pending for execution
        /// </summary>
        /// <returns>Scripts pending for execution</returns>
        IEnumerable<string> GetPendingScripts();

        /// <summary>
        /// Retrieves script of specific file name
        /// </summary>
        /// <param name="fileName">Script file name</param>
        /// <returns>Script of a specific name</returns>
        string GetScript(string fileName);

        /// <summary>
        /// Executes script of the specific name
        /// </summary>
        /// <param name="fileName">Script file name</param>
        void ExecuteScript(string fileName);

        /// <summary>
        /// Executes all pending scripts
        /// </summary>
        void ExecutePendingScripts();
    }
}
