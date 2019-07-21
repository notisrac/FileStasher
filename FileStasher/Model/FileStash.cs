using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStasher.Model
{
    /// <summary>
    /// Represents a file stash
    /// </summary>
    [Serializable]
    public class FileStash : IEnumerable
    {
        /// <summary>
        /// Id of the stash
        /// </summary>
        [JsonProperty]
        public Guid Id { get; private set; }
        /// <summary>
        /// Name of the stash
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty]
        public string Description { get; set; }
        /// <summary>
        /// The date when the stash was created
        /// </summary>
        [JsonProperty]
        public DateTime CreatedDate { get; private set; }
        /// <summary>
        /// List of files in the stash
        /// </summary>
        [JsonProperty]
        public IList<StashedFile> Files { get; set; }

        public FileStash()
        {
            this.Files = new ObservableCollection<StashedFile>();
            // set these for a new stash
            this.CreatedDate = DateTime.UtcNow;
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Adds a file to the stash
        /// </summary>
        /// <param name="file">The file to add</param>
        public void Add(StashedFile file)
        {
            if (!this.Files.Contains(file))
            {
                this.Files.Add(file);
            }
        }

        /// <summary>
        /// Removes a file from the stash
        /// </summary>
        /// <param name="file">The file to remove</param>
        public void Remove(StashedFile file)
        {
            if (this.Files.Contains(file))
            {
                this.Files.Remove(file);
            }
        }

        /// <summary>
        /// Updates a file in the stash
        /// </summary>
        /// <param name="file">The file to update</param>
        public void Update(StashedFile file)
        {
            if (this.Files.Contains(file))
            {
                var index = this.Files.IndexOf(file);
                this.Files[index] = file;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.Files.GetEnumerator();
        }
    }
}
