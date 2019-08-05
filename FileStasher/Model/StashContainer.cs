using Newtonsoft.Json;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStasher.Model
{
    /// <summary>
    /// A collection of file stashes
    /// </summary>
    [Serializable]
    public class StashContainer : NotifyDataChangedBase, IEnumerable
    {
        /// <summary>
        /// List of stashes
        /// </summary>
        public IList<FileStash> Stashes { get; set; }
        /// <summary>
        /// The full name of the file where this container is
        /// </summary>
        public string FileName { get; set; }


        private ILogger logger;


        public StashContainer()
        {
            this.Stashes = new ObservableCollection<FileStash>();
        }

        public StashContainer(ILogger logger) : this()
        {
            this.logger = logger;
        }

        public void Add()
        {
            this.Stashes.Add(new FileStash() {
                Name = $"Stash {this.Stashes.Count + 1}"
            });
            base.NotifyCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        #region file_operations
        /// <summary>
        /// Loads a <see cref="StashContainer"/> from the specified file
        /// </summary>
        /// <param name="fileName">Full path of the file</param>
        /// <returns>An instance of the stash container created from the file</returns>
        public static StashContainer LoadFromFile(string fileName, ILogger logger)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("Filename cannot be null or empty!");
            }

            if (File.Exists(fileName))
            {
                throw new FileNotFoundException($"File \"{fileName}\" does not exist!");
            }

            try
            {
                var fileContents = File.ReadAllText(fileName);
                var stashContainer = JsonConvert.DeserializeObject<StashContainer>(fileContents);
                stashContainer.logger = logger;

                return stashContainer;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error while loading StashContainer from file \"{fileName}\"!");
                throw;
            }
        }

        /// <summary>
        /// Saves this stash to a file
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveToFile(string fileName)
        {
            try
            {
                string serializedObject = JsonConvert.SerializeObject(this);
                File.WriteAllText(fileName, serializedObject, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error while saving StashContainer to file \"{fileName}\"!");
                throw;
            }
        }
        #endregion

        // <inheritdoc />
        public IEnumerator GetEnumerator()
        {
            return this.Stashes.GetEnumerator();
        }
    }
}
