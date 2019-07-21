using FileStasher.Model;
using FileStasher.Utilities.Debugging;
using NLog;
using NLog.Fluent;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FileStasher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // the location where the app state file should be saved
        private readonly string APPSTATE_FILEPATH = ""; // TODO add a location

        /// <summary>
        /// The IoC container
        /// </summary>
        public Container container { get; private set; }
        /// <summary>
        /// This stores the state of the application
        /// </summary>
        public ApplicationState AppState { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // set up the data binding debugging
            PresentationTraceSources.Refresh();
            PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());
            PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());
            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Warning | SourceLevels.Error;

            // instantiate the container
            this.container = new Container();
            //this.container.Options.DefaultLifestyle = new ThreadScopedLifestyle();
            // register the types
            this.container.Register<ILogger>(() => {
                return NLog.LogManager.GetLogger("default");
            });

            // try to load the application state
            this.AppState = ApplicationState.Load(APPSTATE_FILEPATH, true);

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // save the application state
            this.AppState.Save();

            base.OnExit(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = container.GetInstance<MainWindow>();
            //Application.Current.MainWindow = mainWindow;

            if (e.Args.Length == 1)
            { // open the file from the command line
                mainWindow.Initialize(e.Args[0]);
            }
            else if (!string.IsNullOrWhiteSpace(this.AppState.LastOpenFile))
            {
                mainWindow.Initialize(this.AppState.LastOpenFile);
            }
            mainWindow.Show();
        }
    }
}
