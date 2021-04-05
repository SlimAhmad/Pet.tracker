using Pet.Tracker.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public class RecieveViewModel : BaseViewModel
    {

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand ApproveCommand { get; set; }

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand NextCommand { get; set; }

        public RecieveViewModel()
        {
            // Create commands
            NextCommand = new RelayCommand(async () => await NextAsync());
            ApproveCommand = new RelayCommand(async () => await ApproveAsync());
        }
        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task ApproveAsync()
        {
          
            // Go to register page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Transfer);

            await Task.Delay(1);
        }

        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task NextAsync()
        {

            // Go to register page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Approve);

            await Task.Delay(1);
        }
    }
}
