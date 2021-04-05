using Pet.Tracker.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public class TransferViewModel : BaseViewModel
    {


        #region Commands
        /// <summary>
        /// The command to goto the transferPage
        /// </summary>
        public ICommand TransferCommand { get; set; }


        /// <summary>
        /// The command to goto the recievePage
        /// </summary>
        public ICommand RecieveCommand { get; set; }

        /// <summary>
        /// The command to goto the approve page
        /// </summary>
        public ICommand NextCommand { get; set; }

        #endregion


        #region Default constructor

        public TransferViewModel()
        {
            // Create commands
            NextCommand = new RelayCommand(async () => await NextAsync());
            RecieveCommand = new RelayCommand(async () => await RecievePageAsync());
            TransferCommand = new RelayCommand(async () => await TransferPageAsync());
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Takes the user to the transfer details page
        /// </summary>
        /// <returns></returns>
        public async Task TransferPageAsync()
        {
          
            // Go to transfer details page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.PTransfer);

            await Task.Delay(1);
        }

        /// <summary>
        /// Takes the user to the incoming details page
        /// </summary>
        /// <returns></returns>
        public async Task RecievePageAsync()
        {

            // Go to incoming details page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Incoming);

            await Task.Delay(1);
        }

        /// <summary>
        /// Takes the user to the successful page
        /// </summary>
        /// <returns></returns>
        public async Task ApproveAsync()
        {

            // Go to successful page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Transfer);

            await Task.Delay(1);
        }

        /// <summary>
        /// Takes the user to the approve form page
        /// </summary>
        /// <returns></returns>
        public async Task NextAsync()
        {

            // Go to approve form page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Approve);

            await Task.Delay(1);
        }

        #endregion
    }
}
