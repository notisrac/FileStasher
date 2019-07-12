using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FileStasher.Model
{
    public class StashedFile
    {
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string LastRestorePath { get; set; }
        public int StasherType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Description { get; set; }
        public ImageSource Icon { get; set; }
    }
}
