using Pet.Tracker.Core;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public class FindPetViewModel : BaseViewModel
    {
        public ObservableCollection<MissingPetsApiModel> Items { get; set; }


        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand DetailsCommand { get; set; }

        public FindPetViewModel()
        {

            DetailsCommand = new RelayCommand(async () => await GotoDetailsAsync());

         
        
        }

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

    }
    
}
