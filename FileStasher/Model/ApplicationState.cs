using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStasher.Model
{
    public class ApplicationState
    {
        /// <summary>
        /// List of the recently closed files
        /// </summary>
        public IList<string> RecentlyClosedFiles { get; set; } = new List<string>();
        /// <summary>
        /// The full path of the file, that was last open, when the application closed
        /// </summary>
        public string LastOpenFile { get; set; }
        /// <summary>
        /// The location of the file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Loads the state from a file, or returns an empty object if the file does not exist
        /// </summary>
        /// <param name="fileName">The name and path of the file</param>
        /// <param name="returnNewIfNotExists">Should it return null or an empty object if the file does not exist</param>
        /// <returns>The loaded state object or an empty one, depending on the existance of the file</returns>
        public static ApplicationState Load(string fileName, bool returnNewIfNotExists)
        {
            // TODO implement loading
            return new ApplicationState();
        }

        /// <summary>
        /// Saves the file
        /// </summary>
        public void Save()
        {
            // TODO implement save
        }
    }
}
