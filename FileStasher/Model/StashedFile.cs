using FileStasher.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileStasher.Model
{
    /// <summary>
    /// A single stashed file
    /// </summary>
    [Serializable]
    public class StashedFile : INotifyPropertyChanged
    {
        /// <summary>
        /// Name of the file
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }
        /// <summary>
        /// The path the file was originally taken from
        /// </summary>
        [JsonProperty]
        public string SourcePath { get; set; }
        /// <summary>
        /// The path the file was last time restored to
        /// </summary>
        [JsonProperty]
        public string LastRestorePath { get; set; }
        /// <summary>
        /// The type of the stasher that should be used to handle this file
        /// </summary>
        [JsonProperty]
        public int StasherType { get; set; }
        /// <summary>
        /// The date, this file was added to the stash
        /// </summary>
        [JsonProperty]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// The date, this file was last updated
        /// </summary>
        [JsonProperty]
        public DateTime LastUpdatedDate { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty]
        public string Description { get; set; }
        /// <summary>
        /// The icon of the file
        /// </summary>
        [JsonIgnore]
        public ImageSource Icon { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Creates a stashed file from a fileinfo
        /// </summary>
        /// <param name="file">The source file</param>
        /// <returns>A stashed file object</returns>
        public static StashedFile FromFileInfo(FileInfo file)
        {
            var stashedFile = new StashedFile();
            // load contents!
            stashedFile.Name = file.Name;
            stashedFile.SourcePath = file.FullName;
            stashedFile.CreatedDate = DateTime.UtcNow;
            stashedFile.LastUpdatedDate = DateTime.UtcNow;
            stashedFile.LoadIcon(file.FullName);

            return stashedFile;
        }

        /// <summary>
        /// Loads the icon for the file's extension
        /// </summary>
        /// <param name="fileName">The filename, with the extension</param>
        internal void LoadIcon(string fileName)
        {
            try
            {
                var fileExt = new FileInfo(fileName).Extension;
                var iconBitmap = Icons.IconFromExtension(fileExt, Icons.SystemIconSize.Small)?.ToBitmap();
                if (iconBitmap == null)
                {
                    iconBitmap = Icons.IconFromExtensionShell(fileExt, Icons.SystemIconSize.Small)?.ToBitmap();
                }
                if (iconBitmap != null)
                {
                    this.Icon = ImageUtils.BitmapToBitmapImage(iconBitmap);
                }

            }
            catch (Exception ex)
            {
                // TODO log me
                // TODO set a default icon
            }
        }

        /// <summary>
        /// Loads the icon for the file, after it has been deserialized
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.LoadIcon(this.SourcePath);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
