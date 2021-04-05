using Pet.Tracker.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public class OwnerDetailsViewModel : BaseViewModel
    {

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand RegisterCommand { get; set; }

        public OwnerDetailsViewModel()
        {
            // Create commands
       
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
        }
        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task RegisterAsync()
        {
          
            // Go to Pet Details page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Pet);

            await Task.Delay(1);
        }
    }
}
