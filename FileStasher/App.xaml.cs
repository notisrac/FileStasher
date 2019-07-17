using NLog;
using NLog.Fluent;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        /// <summary>
        /// The IoC container
        /// </summary>
        public static Container container;

        protected override void OnStartup(StartupEventArgs e)
        {
            // instantiate the container
            container = new Container();
            container.Options.DefaultLifestyle = new AsyncScopedLifestyle();
            // register the types
            container.Register<ILogger>(() => {
                return NLog.LogManager.GetLogger("default");
            });

            base.OnStartup(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = container.GetInstance<MainWindow>();

            if (e.Args.Length == 1)
            {
                // TODO open file from command line argument
            }

            mainWindow.Show();
        }
    }
}
