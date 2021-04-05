using Pet.Tracker.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public class PetsDetailsViewModel : BaseViewModel
    {

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand RegisterCommand { get; set; }

        public PetsDetailsViewModel()
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

            var a = Shell.Current.CurrentItem;
            var b = Shell.Current.CurrentState == null;

            // Go to register page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Home);

  

            await Task.Delay(1);
        }
    }
}
 