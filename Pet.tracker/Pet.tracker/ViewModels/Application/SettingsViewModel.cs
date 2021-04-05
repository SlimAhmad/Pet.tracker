using Dna;
using Pet.Tracker.Core;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using static Dna.FrameworkDI;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    /// <summary>
    /// The settings state as a view model
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The text to show while loading text
        /// </summary>
        private string mLoadingText = "...";

        #endregion

        #region Public Properties

        /// <summary>
        /// The current users first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The current users last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The current users username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The current users password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The current users email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The text for the logout button
        /// </summary>
        public string LogoutButtonText { get; set; }


        #region Transactional Properties

        /// <summary>
        /// Indicates if the fields is currently being saved
        /// </summary>
        public bool IsSaving { get; set; }

        /// <summary>
        /// Indicates if the settings details are currently being loaded
        /// </summary>
        public bool SettingsLoading { get; set; }

        /// <summary>
        /// Indicates if the user is currently logging out
        /// </summary>
        public bool LoggingOut { get; set; }

        #endregion

        #endregion

        #region Commands

        /// <summary>
        /// The command to logout of the application
        /// </summary>
        public ICommand LogoutCommand { get; set; }

        /// <summary>
        /// The command to goto the contact page
        /// </summary>
        public ICommand ContactCommand { get; set; }

        /// <summary>
        /// The command to goto the Find pet page
        /// </summary>
        public ICommand FindPetCommand { get; set; }

        /// <summary>
        /// The command to goto the Add Pet page
        /// </summary>
        public ICommand AddPetCommand { get; set; }

        /// <summary>
        /// The command to clear the users data from the view model
        /// </summary>
        public ICommand ClearUserDataCommand { get; set; }

        /// <summary>
        /// Loads the settings data from the client data store
        /// </summary>
        public ICommand LoadCommand { get; set; }

        /// <summary>
        /// Saves the current data to the server
        /// </summary>
        public ICommand SaveCommand { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SettingsViewModel()
        {

            LogoutCommand = new RelayCommand(async () => await LogoutAsync());
            ContactCommand = new RelayCommand(() => ContactAsync());
            AddPetCommand = new RelayCommand(() => GotoAddAsync());
            FindPetCommand = new RelayCommand(() => GotoFindAsync());
            ClearUserDataCommand = new RelayCommand(ClearUserData);
            LoadCommand = new RelayCommand(async () => await LoadAsync());
            SaveCommand = new RelayCommand(async () => await SaveAsync());
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Goes to the contact page
        /// </summary>
        public void ContactAsync()
        {
            // Go to login page
            ViewModelApplication.GoToPageAsync(ApplicationPage.Contact);

        }

        /// <summary>
        /// Goes to the find pet page
        /// </summary>
        public void GotoFindAsync()
        {
            // Go to login page
            ViewModelApplication.GoToPageAsync(ApplicationPage.Find);

        }

        /// <summary>
        /// Goes to the add pet page 
        /// </summary>
        public void GotoAddAsync()
        {
            // Go to login page
            ViewModelApplication.GoToPageAsync(ApplicationPage.Edit);

        }

        /// <summary>
        /// Logs the user out
        /// </summary>
        public async Task LogoutAsync()
        {
            // Lock this command to ignore any other requests while processing
            await RunCommandAsync(() => LoggingOut, async () =>
            {
                // TODO: Confirm the user wants to logout

                // Clear any user data/cache
                await ClientDataStore.ClearAllLoginCredentialsAsync();

                await ClientDataStore.ClearAllPetCredentialsAsync();

                await ClientDataStore.ClearAllVetCentersAsync();

                await ClientDataStore.ClearAllPetsAsync();

                // Clean all application level view models that contain
                // any information about the current user
                ClearUserData();

                // Go to login page
                ViewModelApplication.GoToPageAsync(ApplicationPage.Logout);


            });
        }

        /// <summary>
        /// Clears any data specific to the current user
        /// </summary>
        public void ClearUserData()
        {
            // Clear all view models containing the users info
            FirstName = mLoadingText;
            LastName = mLoadingText;
            Username = mLoadingText;
            Email = mLoadingText;
        }

        /// <summary>
        /// Sets the settings view model properties based on the data in the client data store
        /// </summary>
        public async Task LoadAsync()
        {
            // Lock this command to ignore any other requests while processing
            await RunCommandAsync(() => SettingsLoading, async () =>
            {
                // Store single transcient instance of client data store
                var scopedClientDataStore = ClientDataStore;

                // Update values from local cache
                await UpdateValuesFromLocalStoreAsync(scopedClientDataStore);

                // Get the user token
                var token = (await scopedClientDataStore.GetLoginCredentialsAsync())?.Token;

                // If we don't have a token (so we are not logged in...)
                if (string.IsNullOrEmpty(token))
                    // Then do nothing more
                    return;

                // Load user profile details form server
                var result = await WebRequests.PostAsync<ApiResponse<UserProfileDetailsApiModel>>(
                    // Set URL
                    RouteHelpers.GetAbsoluteRoute(ApiRoutes.GetUserProfile, AppSetting.PetTrackerServer),
                    // Pass in user Token
                    bearerToken: token);

                // If the response has an error...
                if (await result.DisplayErrorIfFailedAsync("Load User Details Failed"))
                    // We are done
                    return;


                // TODO: Should we check if the values are different before saving?

                // Create data model from the response
                var dataModel = result.ServerResponse.Response.ToLoginCredentialsDataModel();

                // Re-add our known token
                dataModel.Token = token;

                // Save the new information in the data store
                await scopedClientDataStore.SaveLoginCredentialsAsync(dataModel);

                // Update values from local cache
                await UpdateValuesFromLocalStoreAsync(scopedClientDataStore);


            });
        }


        /// <summary>
        /// Saves the new First Name to the server
        /// </summary>
        /// <returns>Returns true if successful, false otherwise</returns>
        public async Task<bool> SaveAsync()
        {
            // Lock this command to ignore any other requests while processing
            return await RunCommandAsync(() => IsSaving, async () =>
            {
                // Update the First Name value on the server...
                return await UpdateUserCredentialsValueAsync(
                    // Display name
                    "First Name",
                    // Update the first name
                    (credentials) => credentials.FirstName,
                    // To new value
                    FirstName,
                    // Set Api model value
                    (apiModel, value) => apiModel.FirstName = value
                    );
            });
        }


        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Loads the settings from the local data store and binds them 
        /// to this view model
        /// </summary>
        /// <returns></returns>
        private async Task UpdateValuesFromLocalStoreAsync(IClientDataStore clientDataStore)
        {
            // Get the stored credentials
            var storedCredentials = await clientDataStore.GetLoginCredentialsAsync();

            // Set first name
            FirstName = storedCredentials?.FirstName;

            // Set last name
            LastName = storedCredentials?.LastName;

            // Set username
            Username = storedCredentials?.Username;

            // Set email
            Email = storedCredentials?.Email;
        }


        /// <summary>
        /// Updates a specific value from the client data store for the user profile details
        /// and attempts to update the server to match those details.
        /// For example, updating the first name of the user.
        /// </summary>
        /// <param name="displayName">The display name for logging and display purposes of the property we are updating</param>
        /// <param name="propertyToUpdate">The property from the <see cref="LoginCredentialsDataModel"/> to be updated</param>
        /// <param name="newValue">The new value to update the property to</param>
        /// <param name="setApiModel">Sets the correct property in the <see cref="UpdateUserProfileApiModel"/> model that this property maps to</param>
        /// <returns></returns>
        private async Task<bool> UpdateUserCredentialsValueAsync(string displayName, Expression<Func<LoginCredentialsDataModel, string>> propertyToUpdate, string newValue, Action<UpdateUserProfileApiModel, string> setApiModel)
        {
            // Log it
            Logger.LogDebugSource($"Saving {displayName}...");

            // Get the current known credentials
            var credentials = await ClientDataStore.GetLoginCredentialsAsync();

            // Get the property to update from the credentials
            var toUpdate = propertyToUpdate.GetPropertyValue(credentials);

            // Log it
            Logger.LogDebugSource($"{displayName} currently {toUpdate}, updating to {newValue}");

            // Check if the value is the same. If so...
            if (toUpdate == newValue)
            {
                // Log it
                Logger.LogDebugSource($"{displayName} is the same, ignoring");

                // Return true
                return true;
            }

            // Set the property
            propertyToUpdate.SetPropertyValue(newValue, credentials);

            // Create update details
            var updateApiModel = new UpdateUserProfileApiModel();

            // Ask caller to set appropriate value
            setApiModel(updateApiModel, newValue);

            // Update the server with the details
            var result = await WebRequests.PostAsync<ApiResponse>(
                // Set URL
                RouteHelpers.GetAbsoluteRoute(ApiRoutes.UpdateUserProfile),
                // Pass the Api model
                updateApiModel,
                // Pass in user Token
                bearerToken: credentials.Token);

            // If the response has an error...
            if (await result.DisplayErrorIfFailedAsync($"Update {displayName}"))
            {
                // Log it
                Logger.LogDebugSource($"Failed to update {displayName}. {result.ErrorMessage}");

                // Return false
                return false;
            }

            // Log it
            Logger.LogDebugSource($"Successfully updated {displayName}. Saving to local database cache...");

            // Store the new user credentials the data store
            await ClientDataStore.SaveLoginCredentialsAsync(credentials);

            // Return successful
            return true;
        }

        #endregion
    }
}
