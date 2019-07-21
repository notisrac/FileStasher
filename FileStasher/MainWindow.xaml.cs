using FileStasher.Model;
using Microsoft.Win32;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileStasher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public StashContainer stashContainer { get; set; }
        public string CurrentlyOpenFileName { get; set; }

        
        private readonly ILogger logger;


        public MainWindow(ILogger logger)
        {
            InitializeComponent();

            this.logger = logger;
            this.stashContainer = new StashContainer();
            this.CurrentlyOpenFileName = string.Empty;

            this.DataContext = this;

            // temp
            var stash = new FileStash()
            {
                Description = "asdasdqefwefsvsv",
                Name = "test stash"
            };
            stash.Files.Add(new StashedFile() {
                Description = "asdasdererr2wwccs",
                LastRestorePath = "c:\\temp.txt",
                Name = "temp.txt",
                SourcePath = "c:\\temp.txt"
            });
            stash.Files.Add(StashedFile.FromFileInfo(new System.IO.FileInfo(@"c:\Users\noti\Dropbox\Projects\FileStasher\FileStasher\packages.config")));
            stash.Files.Add(StashedFile.FromFileInfo(new System.IO.FileInfo(@"c:\Users\noti\Dropbox\Projects\FileStasher\FileStasher\FileStasher.csproj")));
            this.stashContainer.Stashes.Add(stash);
            this.stashContainer.Add();
            this.CurrentlyOpenFileName = "c:\\temp.txt";
        }

        public void Initialize(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                this.Load(fileName);
            }
        }

        public void New()
        {
            // TODO show the file open dialog, and get the filename from it
            this.stashContainer = new StashContainer();
            this.CurrentlyOpenFileName = "c:\test.json";
        }

        public void Unload()
        {
            // TODO check if there is a container loaded, or modified
            // TODO if the collection was modified, ask to save
            this.stashContainer = null;
            this.CurrentlyOpenFileName = null;
        }

        public void Load(string fileName)
        {
            this.Unload();
            if (!string.IsNullOrWhiteSpace(fileName))
            { // try to load the file
                this.CurrentlyOpenFileName = fileName;
                try
                {
                    this.stashContainer = StashContainer.LoadFromFile(fileName, logger);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not load file \"{fileName}\": \r\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                this.stashContainer = new StashContainer();
                this.CurrentlyOpenFileName = null;
            }
        }

        public void Save(string fileName)
        {
            this.stashContainer.SaveToFile(fileName);
            // this is the save as functionality
            if (0 != string.Compare(fileName, this.CurrentlyOpenFileName))
            {
                this.CurrentlyOpenFileName = fileName;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Unload();

            base.OnClosing(e);
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.New();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO show file loading dialog
            OpenFileDialog openFileDialog = new OpenFileDialog() {
                Filter = "Stash containers (*.json)|*.json|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == true)
            {
                this.Load(openFileDialog.FileName);
            }
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void btnAddStash_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("adding new stash!!!!!!!!!!!!!!!");
            this.stashContainer.Add();
            //this.trvList.GetBindingExpression(TreeView.ItemsSourceProperty).UpdateSource();
        }
    }
}
