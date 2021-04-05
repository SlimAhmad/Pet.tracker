using Pet.Tracker.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public class ContactViewModel : BaseViewModel
    {

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand RegisterCommand { get; set; }

        public ContactViewModel()
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
          
            // Go to register page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Register);

            await Task.Delay(1);
        }
    }
}
