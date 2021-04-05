using Dna;
using Microsoft.Extensions.Logging;
using Pet.Tracker.Core;
using Pet.Tracker.Relational;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Dna.FrameworkDI;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public partial class App : Application
    {


        public string DbPath { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbPath"> The Database path</param>
        public App(string dbPath)
        {
            InitializeComponent();

            DbPath = dbPath;

            //Logger.LogDebugSource($"The DataBase Path is Located at {DbPath}");

        }

        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStart()
        {
            // Let the base application do what it needs
            base.OnStart();

            // Setup the main application 
            await ApplicationSetupAsync();

            // Log it
            Logger.LogDebugSource("Application starting...");

            // Show the main page
            Current.MainPage = new MainPage();

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        #region Helper methods

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private async Task ApplicationSetupAsync()
        {
            // Setup the Dna Framework
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddClientDataStore(DbPath)
                .AddPetTrackerViewModels()
                .Build();

            // Ensure the client data store 
            await ClientDataStore.EnsureDataStoreAsync();



        }



        /// <summary>
        /// Monitors the Pet trackers website is up, running and reachable
        /// by periodically hitting it up
        /// </summary>
        private void MonitorServerStatus()
        {
            // Create a new endpoint watcher
            var httpWatcher = new HttpEndpointChecker(
                // Checking fasetto.chat
                AppSetting.PetTrackerServer,
                // Every 20 seconds
                interval: 20000,
                // Pass in the DI logger
                logger: Framework.Provider.GetService<ILogger>(),
                // On change...
                stateChangedCallback: (result) =>
                {
                    // Update the view model property with the new result
                    ViewModelApplication.ServerReachable = result;
                });
        }

        #endregion
    }
}
