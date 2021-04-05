using Dna;
using Pet.Tracker.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    public class RegisterViewModel : BaseViewModel
    {


        #region Public Properties

        /// <summary>
        /// The username of the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// A flag indicating if the register command is running
        /// </summary>
        public bool RegisterIsRunning { get; set; }

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

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RegisterViewModel()
        {
            // Create commands
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            // Go to register page?
            ViewModelApplication.GoToPageAsync(ApplicationPage.Login);

            await Task.Delay(1);
        }


        /// <summary>
        /// Attempts to register a new user
        /// </summary>
        /// <returns></returns>
        public async Task RegisterAsync()
        {
            await RunCommandAsync(() => RegisterIsRunning, async () =>
            {
                // Call the server and attempt to register with the provided credentials
                var result = await WebRequests.PostAsync<ApiResponse<RegisterResultApiModel>>(
                // Set URL
                    RouteHelpers.GetAbsoluteRoute(ApiRoutes.Register, AppSetting.PetTrackerServer),
                    // Create api model
                    new RegisterCredentialsApiModel
                    {
                        Username = Username,
                        Email = Email,
                        Password = Password
                    });

                // If the response has an error...
                if (await result.DisplayErrorIfFailedAsync("Registration Failed"))
                    // We are done
                    return;

                // OK successfully registered (and logged in)... now get users data
                var registerResult = result.ServerResponse.Response;

                // Let the application view model handle what happens
                // with the successful registration
                await ViewModelApplication.HandleSuccessfulRegistrationAsync(registerResult);
            });
        }

    }
}
