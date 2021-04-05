using Pet.Tracker.Core;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{

    [QueryProperty("Age", "age")]
    [QueryProperty("Description", "description")]
    [QueryProperty("Weight", "weight")]
    [QueryProperty("Height", "height")]
    [QueryProperty("Gender", "gender")]
    [QueryProperty("Breed", "breed")]
    [QueryProperty("Name", "name")]
    [QueryProperty("Status", "status")]
    public  class TransferPetsDetailsViewModel : BaseViewModel
    {

        #region Private fields

        /// <summary>
        /// 
        /// </summary>
        private string age;

        /// <summary>
        /// 
        /// </summary>
        private string breed;

        /// <summary>
        /// 
        /// </summary>
        private string height;

        /// <summary>
        /// 
        /// </summary>
        private string weight;

        /// <summary>
        /// 
        /// </summary>
        private string status;

        /// <summary>
        /// 
        /// </summary>
        private string gender;

        /// <summary>
        /// 
        /// </summary>
        private string name;

        /// <summary>
        /// 
        /// </summary>
        private string description;
        #endregion



        #region public commands

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand ContactCommand { get; set; }

        #endregion



        #region public properties

        /// <summary>
        /// 
        /// </summary>
        public string Age
        {
            get => age;
            set => age = Uri.UnescapeDataString(value);

        }

        /// <summary>
        /// 
        /// </summary>
        public string Gender
        {
            get => gender;
            set
            {
                gender = Uri.UnescapeDataString(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Breed
        {
            get
            {
                return breed;
            }

            set
            {
                breed = Uri.UnescapeDataString(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Height
        {
            get => height;
            set => height = Uri.UnescapeDataString(value);
        }


        /// <summary>
        /// 
        /// </summary>
        public string Weight
        {
            get => weight;
            set
            {
                weight = Uri.UnescapeDataString(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = Uri.UnescapeDataString(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Status
        {
            get => status;
            set
            {
                status = Uri.UnescapeDataString(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get => description;
            set
            {
                description = Uri.UnescapeDataString(value);
            }
        }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public TransferPetsDetailsViewModel()
        {

            

            ContactCommand = new RelayCommand(async () => await ContactAsync());
        }

        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task ContactAsync()
        {



            // Go to register page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Transfer);

            await Task.Delay(1);
        }

        
   

      
     
    }
}
