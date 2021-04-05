using Dna;
using Pet.Tracker.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{

    public class VetCentersViewModel : BaseViewModel
    {
        #region private members

        private VetCentersApiModel Selected { get; set; }

        #endregion

        #region public properties

        /// <summary>
        /// The collection of vet and their centers
        /// </summary>
        public ObservableCollection<VetCentersApiModel> VetCenters { get; set; }

        /// <summary>
        /// The collection of vet and their centers
        /// </summary>
        public ObservableCollection<VetCentersApiModel> Centers { get; set; }

        public bool CentersLoading { get; set; }
        #endregion


        #region public commands

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand ContactCommand { get; set; }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public VetCentersViewModel()
        {


            Centers = new ObservableCollection<VetCentersApiModel>
            {
                new VetCentersApiModel
                {
                    Title = "Alpha Vet Center",
                    Area = "Asokoro",
                    State = "Abuja",
                    Address = "No 1, Nairobi street, opposite hall guard"
                },

                new VetCentersApiModel
                {
                    Title = "Aroro Vet Center",
                    Area = "Gwarinpa",
                    State = "Abuja",
                    Address = "No 1b, Sapelle street, opposite hall guard"
                },

                new VetCentersApiModel
                {
                    Title = "AFro Manager",
                    Area = "Jabi",
                    State = "Abuja",
                    Address = "No 2a, Kenya street"
                },

            };

            Task.Run(async () => await LoadAsync());
            Task.Run(async () => await UpdatePetValuesFromLocalStoreAsync(ClientDataStore));
            ContactCommand = new RelayCommand(async () => await ContactAsync());
        }

        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task ContactAsync()
        {



            // Go to register page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Centers);

            await Task.Delay(1);
        }


        /// <summary>
        /// Takes the user to the details page
        /// </summary>
        public VetCentersApiModel SelectedItem
        {

            get { return Selected; }

            set
            {

                if (Selected != value)
                {

                    Selected = value;

                    //TODO localize string parameters
                    var parameter = $"?address={Selected.Address}&id={Selected.Id}&state={Selected.State}&area={Selected.Area}&title={Selected.Title}";

                    // Go to register page?
                    ViewModelApplication.GoToPageAsync(ApplicationPage.Centers, null, true, false, parameter);

                    //Set the selected item to null to deselect items
                    Selected = null;

                }

            }
        }


        /// <summary>
        /// Sets the Home view model properties based on the data in the client data store
        /// </summary>
        public async Task LoadAsync()
        {
            // Lock this command to ignore any other requests while processing
            await RunCommandAsync(() => CentersLoading, async () =>
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
                var pet_result = await WebRequests.PostAsync<ApiResponse<VetCentersResultsApiModel>>(
                    // Set URL
                    RouteHelpers.GetAbsoluteRoute(CentersApiRoutes.GetAllCenters, AppSetting.PetTrackerServer),
                    // Pass in user Token
                    bearerToken: token);

                // If the response has an error...
                if (await pet_result.DisplayErrorIfFailedAsync("Load missing Pet Details Failed"))
                    // We are done
                    return;


                // Create a new list of results
                var results = new VetCentersResultsApiModel();

                // Add each centers details
                results.AddRange(pet_result.ServerResponse.Response.Select(u => new VetCenterResultApiModel
                {

                    Area = u.Area,
                    Address = u.Address,
                    State = u.State,
                    Title = u.Title,
                    Id = u.Id

                }));

                // Save the new information in the data store
                await scopedClientDataStore.SaveVetCentersAsync(results);


                // Update values from local cache
                await UpdatePetValuesFromLocalStoreAsync(scopedClientDataStore);



            });
        }

        #region Private helpers

        /// <summary>
        /// Loads the settings from the local data store and binds them 
        /// to this view model
        /// </summary>
        /// <returns></returns>
        private async Task UpdatePetValuesFromLocalStoreAsync(IClientDataStore clientDataStore)
        {
            // Get the stored credentials
            var storedCredentials = await clientDataStore.GetVetCentersAsync();

            VetCenters = new ObservableCollection<VetCentersApiModel>();

            storedCredentials.ForEach((a) =>
            {

                var b = new VetCenterResultApiModel
                {
                    Address = a.Address,
                    Title = a.Title,
                    Id = a.Id,
                    Area = a.Area,
                    State = a.State
                };
                VetCenters.Add(b);

            }
            );



        }
        #endregion

    }
}
