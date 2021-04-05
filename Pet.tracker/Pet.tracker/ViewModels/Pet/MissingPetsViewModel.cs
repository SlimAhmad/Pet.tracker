using Dna;
using Pet.Tracker.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public class MissingPetsViewModel : BaseViewModel
    {
        public ObservableCollection<MissingPetsApiModel> Pets { get; set; }

        public ObservableCollection<MissingPetsApiModel> Items { get; set; }

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand DetailsCommand { get; set; }

        private MissingPetsApiModel Selected { get; set; }


        public bool PetsLoading { get; set; }

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MissingPetsViewModel()
        {
            Task.Run(async () => await LoadAsync());

            DetailsCommand = new RelayCommand(async () => await GotoDetailsAsync());

            Items = new ObservableCollection<MissingPetsApiModel>
            {
                new MissingPetsApiModel
                {
                   Age =3,
                   Breed = "bull dog",
                   Description = "Bad Dog",
                   Gender = "Male",
                   Image = "dog.jpg",
                   Name = "Salim",
                   District = "Gwarinpa",
                   Status = "Missing"
                },
                new MissingPetsApiModel
                {
                  Age =3,
                  Breed = "bull dog",
                  Description = "Bad Dog",
                   Gender = "Male",
                   Image = "dog.jpg",
                   Name = "Ahmad",
                    District = "Gwarinpa",
                    Status = "Missing"
                },
                new MissingPetsApiModel
                {
                  Age =3,
                  Breed = "bull dog",
                  Description = "Bad Dog",
                   Gender = "Male",
                   Image = "dog.jpg",
                   Name = "Fa-reed",
                    District = "Gwarinpa",
                    Status = "Missing"
                }
            };

        }

        #endregion

        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task GotoDetailsAsync()
        {

            // Go to register page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Missing);

            await Task.Delay(1);
        }

        /// <summary>
        /// Takes the user to the details page
        /// </summary>
        public MissingPetsApiModel SelectedItem
        {
            get { return Selected; }

            set
            {

                if (Selected != value)
                {
                    Selected = value;

                    // Go to register page?
                    ViewModelApplication.GoToPageAsync(ApplicationPage.Missing);
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
            await RunCommandAsync(() => PetsLoading, async () =>
            {
                // Store single transcient instance of client data store
                var scopedClientDataStore = ClientDataStore;

                // Update values from local cache
                await UpdatePetValuesFromLocalStoreAsync(scopedClientDataStore);

                // Get the pets id
                // var id = (await scopedClientDataStore.GetPetCredentialsAsync())?.Id;

                // Get the user token
                var token = (await scopedClientDataStore.GetLoginCredentialsAsync())?.Token;

                // If we don't have a token (so we are not logged in...)
                if (string.IsNullOrEmpty(token))
                    // Then do nothing more
                    return;

                // Load user profile details form server
                var pet_result = await WebRequests.PostAsync<ApiResponse<TransferPetsResultsApiModel>>(
                    // Set URL
                    RouteHelpers.GetAbsoluteRoute(PetApiRoutes.GetMissingPets, AppSetting.PetTrackerServer),
                    // Pass in user Token
                    bearerToken: token);

                // If the response has an error...
                if (await pet_result.DisplayErrorIfFailedAsync("Load missing Pet Details Failed"))
                    // We are done
                    return;

                // Create a new list of results
                var results = new TransferPetsResultsApiModel();


                // Add each Pets details
                results.AddRange(pet_result.ServerResponse.Response.Select(u => new TransferPetResultApiModel
                {


                    Neutered = u.Neutered,
                    Age = u.Age,
                    Breed = u.Breed,
                    Height = u.Height,
                    Weight = u.Weight,
                    Username = u.Username,
                    Transfer = u.Transfer,
                    District = u.District,
                    Email = u.Email,
                    Gender = u.Gender,
                    Image = u.Image,
                    Description = u.Description,
                    PetId = u.PetId,
                    PetOwnerName = u.PetOwnerName,
                    Phone = u.Phone,
                    Name = u.Name,
                    Status = u.Status,
                    TransferEmail = u.TransferEmail,
                    TransferPhone = u.TransferPhone




                }));
                // Save the new information in the data store
                await scopedClientDataStore.SavePetsAsync(results);


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
            var storedCredentials = await clientDataStore.GetPetsAsync();


            Pets = new ObservableCollection<MissingPetsApiModel>();

            storedCredentials.ForEach((u) =>
            {

                var b = new MissingPetsApiModel
                {
                    Neutered = u.Neutered,
                    Age = u.Age,
                    Breed = u.Breed,
                    Height = u.Height,
                    Weight = u.Weight,


                    District = u.District,

                    Gender = u.Gender,
                    Image = u.Image,
                    Description = u.Description,

                    Name = u.Name,
                    Status = u.Status,

                };
                Pets.Add(b);

            }
            );

        }
        #endregion
    }

}
