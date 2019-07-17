using FileStasher.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileStasher.Components
{
    public class FileStashRepo
    {
        /// <summary>
        /// List of the stashes in the current container
        /// </summary>
        public List<FileStash> Stashes { get { return Container?.Stashes?.ToList(); } }
        /// <summary>
        /// The current container
        /// </summary>
        public StashContainer Container { get; private set; }
        /// <summary>
        /// The filename of the currently loaded stash container
        /// </summary>
        public string FileName { get; private set; }

        private readonly ILogger logger;

        public FileStashRepo(ILogger logger)
        {
            this.logger = logger;
            // by default a new container will be created
            this.New();
        }

        /// <summary>
        /// Creates a new <see cref="StashContainer"/>
        /// </summary>
        public void New()
        {
            this.Container = new StashContainer(logger);
            this.FileName = null;
        }

        /// <summary>
        /// Loads the stash container from a file
        /// </summary>
        /// <param name="fileName">Name of the file to load</param>
        public void Load(string fileName)
        {
            this.Container = StashContainer.LoadFromFile(fileName, this.logger);
            this.FileName = fileName;
        }

        /// <summary>
        /// Saves the current stash container into a file
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        public void Save(string fileName = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = this.FileName;
            }
            this.Container.SaveToFile(fileName);
        }

        /// <summary>
        /// Returns a stash by its id
        /// </summary>
        /// <param name="id">The <see cref="string"/> representation of the GUID id</param>
        /// <returns>The <see cref="FileStash"/></returns>
        public FileStash GetById(string id)
        {
            if (Guid.TryParse(id, out var val))
            {
                return this.GetById(val);
            }

            return null;
        }

        /// <summary>
        /// Returns a stash by its id
        /// </summary>
        /// <param name="id">The id of the stash</param>
        /// <returns>The <see cref="FileStash"/> with the id</returns>
        public FileStash GetById(Guid id)
        {
            return this.Stashes?.SingleOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Returns all the <see cref="FileStash"/>es if the container
        /// </summary>
        /// <returns>All the <see cref="FileStash"/> in the container</returns>
        public IEnumerable<FileStash> List()
        {
            return this.Stashes?.AsEnumerable();
        }

        /// <summary>
        /// Returns all the <see cref="FileStash"/>es if the container, that matches the search criteria
        /// </summary>
        /// <param name="predicate">The search criteria</param>
        /// <returns>All the <see cref="FileStash"/> in the container</returns>
        public IEnumerable<FileStash> List(Func<FileStash, bool> predicate)
        {
            return this.Stashes?.Where(predicate).AsEnumerable();
        }

        /// <summary>
        /// Adds a <see cref="FileStash"/> to the current collection
        /// </summary>
        /// <param name="stash">THe <see cref="FileStash"/> to add</param>
        public void Add(FileStash stash)
        {
            if (stash == null)
            {
                return;
            }

            if (!this.Container.Stashes.Contains(stash))
            {
                this.Container.Stashes.Add(stash);
            }
            else
            {
                this.Update(stash);
            }
        }

        /// <summary>
        /// Removes a <see cref="FileStash"/> from the collection
        /// </summary>
        /// <param name="stash">The <see cref="FileStash"/> to remove</param>
        public void Remove(FileStash stash)
        {
            if (stash == null)
            {
                return;
            }

            if (this.Container.Stashes.Contains(stash))
            {
                this.Container.Stashes.Remove(stash);
            }
        }

        /// <summary>
        /// Updates a <see cref="FileStash"/> in the collection
        /// </summary>
        /// <param name="stash">The <see cref="FileStash"/> to update</param>
        public void Update(FileStash stash)
        {
            if (stash == null)
            {
                return;
            }

            var val = this.Container.Stashes.Where(s => s.Id == stash.Id).SingleOrDefault();
            if (val != null)
            {
                val = stash;
            }
        }
    }
}
