using Dna;
using Pet.Tracker.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public class LoginViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }



        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// A flag indicating if the login command is running
        /// </summary>
        public bool LoginIsRunning { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand RegisterCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginViewModel()
        {
            // Create commands
            LoginCommand = new RelayCommand(async () => await LoginAsync());
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
        }

        #endregion

        /// <summary>
        /// Attempts to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the users password</param>
        /// <returns></returns>
        public async Task LoginAsync()
        {
            await RunCommandAsync(() => LoginIsRunning, async () =>
            {
                // Call the server and attempt to login with credentials
                var result = await WebRequests.PostAsync<ApiResponse<UserProfileDetailsApiModel>>(
                   // Set URL
                   RouteHelpers.GetAbsoluteRoute(ApiRoutes.Login, AppSetting.PetTrackerServer),
                    // Create api model
                    new LoginCredentialsApiModel
                    {
                        UsernameOrEmail = Email,
                        Password = Password
                    });

                // If the response has an error...
                if (await result.DisplayErrorIfFailedAsync("Login Failed"))
                    // We are done
                    return;

                // OK successfully logged in... now get users data
                var loginResult = result.ServerResponse.Response;

                // Let the application view model handle what happens
                // with the successful login
                await ViewModelApplication.HandleSuccessfulLoginAsync(loginResult);
            });
        }

        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task RegisterAsync()
        {
            var b = Shell.Current.CurrentState;
            // Go to register page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Register);

            await Task.Delay(1);
        }
    }
}
