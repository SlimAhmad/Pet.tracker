using Dna;
using Pet.Tracker.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public class HomeViewModel : BaseViewModel
    {

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Breed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double? Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Neutered { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates if the settings details are currently being loaded
        /// </summary>
        public bool HomeLoading { get; set; }
        #endregion


        #region Commands
        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand SettingsCommand { get; set; }

        /// <summary>
        /// Loads the settings data from the client data store
        /// </summary>
        public ICommand LoadCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public HomeViewModel()
        {
            // Create commands

            SettingsCommand = new RelayCommand(async () => await GotoSettingsAsync());
            LoadCommand = new RelayCommand(async () => await LoadAsync());
            Task.Run(async () => await UpdatePetValuesFromLocalStoreAsync(ClientDataStore));

        }

        #endregion

        #region Commands Methods

        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task GotoSettingsAsync()
        {
            // Go to register page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Settings);

            await Task.Delay(1);
        }

        /// <summary>
        /// Sets the Home view model properties based on the data in the client data store
        /// </summary>
        public async Task LoadAsync()
        {
            // Lock this command to ignore any other requests while processing
            await RunCommandAsync(() => HomeLoading, async () =>
            {
                // Store single transcient instance of client data store
                var scopedClientDataStore = ClientDataStore;

                // Update values from local cache
                await UpdatePetValuesFromLocalStoreAsync(scopedClientDataStore);

                // Get the user token
                var token = (await scopedClientDataStore.GetLoginCredentialsAsync())?.Token;

                // If we don't have a token (so we are not logged in...)
                if (string.IsNullOrEmpty(token))
                    // Then do nothing more
                    return;

                // Load user profile details form server
                var pet_result = await WebRequests.PostAsync<ApiResponse<PetsProfileApiModel>>(
                    // Set URL
                    RouteHelpers.GetAbsoluteRoute(PetApiRoutes.GetPetProfile, AppSetting.PetTrackerServer),
                    // Pass in user Token
                    bearerToken: token);

                // If the response has an error...
                if (await pet_result.DisplayErrorIfFailedAsync("Load Pet Details Failed"))
                    // We are done
                    return;

                // Create data model from the response
                var petDataModel = pet_result.ServerResponse.Response.ToPetsCredentialsDataModel();

                // Save the new information in the data store
                await scopedClientDataStore.SavePetCredentialsAsync(petDataModel);


                // Update values from local cache
                await UpdatePetValuesFromLocalStoreAsync(scopedClientDataStore);



            });
        }


        #endregion


        #region Private helpers

        /// <summary>
        /// Loads the settings from the local data store and binds them 
        /// to this view model
        /// </summary>
        /// <returns></returns>
        private async Task UpdatePetValuesFromLocalStoreAsync(IClientDataStore clientDataStore)
        {
            // Get the stored credentials
            var storedCredentials = await clientDataStore.GetPetCredentialsAsync();

            // Set first name
            Age = storedCredentials?.Age;

            // Set last name
            Breed = storedCredentials?.Breed;

            // Set username
            Weight = storedCredentials?.Weight;

            // Set email
            Height = storedCredentials?.Height;

            // Set email
            Image = storedCredentials?.Image;

            // Set email
            Neutered = storedCredentials?.Neutered;

            // Set email
            Gender = storedCredentials?.Gender;

            // Set email
            Name = storedCredentials?.Name;

            Description = storedCredentials?.Description;

        }
        #endregion

    }
}
