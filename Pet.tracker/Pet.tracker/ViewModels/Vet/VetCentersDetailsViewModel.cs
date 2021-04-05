using Pet.Tracker.Core;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{

    [QueryProperty("Address", "address")]
    [QueryProperty("Id", "id")]
    [QueryProperty("State", "state")]
    [QueryProperty("Area", "area")]
    [QueryProperty("Title", "title")]
    public  class VetCentersDetailsViewModel : BaseViewModel
    {

        #region Private fields

        /// <summary>
        /// 
        /// </summary>
        private string address;

        /// <summary>
        /// 
        /// </summary>
        private string id;

        /// <summary>
        /// 
        /// </summary>
        private string state;

        /// <summary>
        /// 
        /// </summary>
        private string area;

        /// <summary>
        /// 
        /// </summary>
        private string title;
        #endregion

        #region public properties

        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            get => address;
            set => address = Uri.UnescapeDataString(value);

        }

        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            get => id;
            set
            {
                id = Uri.UnescapeDataString(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            get
            {
                return state;
            }

            set
            {
                state = Uri.UnescapeDataString(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Area 
        {
            get => area;
            set => area = Uri.UnescapeDataString(value);
        }


        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get => title;
            set
            {
                title = Uri.UnescapeDataString(value);
            }
        }
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
        public VetCentersDetailsViewModel()
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
            ViewModelApplication.GoToPageAsync(ApplicationPage.Centers);

            await Task.Delay(1);
        }

        
   

      
     
    }
}
