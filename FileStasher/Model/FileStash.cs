using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStasher.Model
{
    public class FileStash
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; private set; }
        public ObservableCollection<StashedFile> Files { get; set; }
    }
}
