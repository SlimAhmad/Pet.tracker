using Pet.Tracker.Core;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{

    public class IncomingPetViewModel : BaseViewModel
    {
        #region public properties

        /// <summary>
        /// The collection of vet and their centers
        /// </summary>
        public ObservableCollection<MissingPetsApiModel> Items { get; set; }


        #endregion



        #region public commands

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand ContactCommand { get; set; }

        #endregion

        private MissingPetsApiModel Selected { get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public IncomingPetViewModel()
        {

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
        public MissingPetsApiModel SelectedItem
        {

            get { return Selected; }

            set
            {

                if (Selected != value)
                {

                    Selected = value;

                    //TODO localize string parameters
                    var parameter = $"?Age={Selected.Age}&id={Selected.Id}&breed={Selected.Breed}&description={Selected.Description}" +
                        $"&gender={Selected.Gender}&name={Selected.Name}&height={Selected.Height}&status={Selected.Status}&weight={Selected.Weight}";

                    // Go to register page?
                    ViewModelApplication.GoToPageAsync(ApplicationPage.RDetails, null, true, false, parameter);

                    //Set the selected item to null to deselect items
                    Selected = null;

                }

            }
        }



    }
}
