using Pet.Tracker.Core;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Pet.Tracker.DI;
namespace Pet.Tracker
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Private Members     



        #endregion

        #region Public Properties



        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        /// <summary>
        /// The view model to use for the current page when the CurrentPage changes
        /// NOTE: This is not a live up-to-date view model of the current page
        ///       it is simply used to set the view model of the current page 
        ///       at the time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        /// <summary>
        /// Determines if the application has to use routing or navigation to pages 
        /// </summary>
        public bool Routing { get; set; }

        /// <summary>
        /// Determines if the application has network access to the Pet Tracker server
        /// </summary>
        public bool ServerReachable { get; set; } = true;

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor
        /// </summary>
        public ApplicationViewModel()
        {

        }

        #endregion

        #region Public Helper Methods

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        /// <param name="viewModel">The view model, if any, to set explicitly to the new page</param>
        /// <param name="routing">routing is enabled use the shell routing mechanism if true</param>
        /// <param name="modal"> Navigates to a modal page if true</param>
        /// <param name="parameter">The string parameters to pass to the next page</param>
        public void GoToPageAsync(ApplicationPage page, BaseViewModel viewModel = null, bool routing = true, bool modal = false, string parameter = null)
        {


            // Navigates to a modal page if true
            if (modal)
                ///
                Shell.Current.Navigation.PushModalAsync(page.ToBasePage());


            // Set the view model
            CurrentPageViewModel = viewModel;

            //routing is enabled use the shell routing mechanism if true
            //if false navigates by pushing to the stack
            if (routing)
                //
                Shell.Current.GoToAsync(page.ToRoutePage() + parameter);
            else
                //push the page to the top of the stack
                Shell.Current.Navigation.PushAsync(page.ToBasePage());



            // See if page has changed
            var different = CurrentPage != page;


            // If the page hasn't changed, fire off notification
            // So pages still update if just the view model has changed
            if (!different)
                OnPropertyChanged(nameof(CurrentPage));


        }



        /// <summary>
        /// Removes a page from the navigation stack
        /// </summary>
        public void GoBackAsync()
        {
            Shell.Current.Navigation.PopAsync();
        }


        /// <summary>
        /// Removes a modal page from the navigation stack
        /// </summary>
        public void GoBackModalAsync()
        {
            Shell.Current.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Called by the views OnAppearing method 
        /// checks if we have valid credentials and navigates to the appropriate page
        /// </summary>
        public async void Init()
        {
            //if we have credentials navigate
            if (await ClientDataStore.HasCredentialsAsync())
                //
                await Shell.Current.GoToAsync("//main");
            else
                // credentials wasn't found navigate
                await Shell.Current.GoToAsync("login");
        }


        /// <summary>
        /// Handles what happens when we have successfully logged in
        /// </summary>
        /// <param name="loginResult">The results from the successful login</param>
        public async Task HandleSuccessfulLoginAsync(UserProfileDetailsApiModel loginResult)
        {
            // Store this in the client data store
            await ClientDataStore.SaveLoginCredentialsAsync(loginResult.ToLoginCredentialsDataModel());

            // Load new settings
            await ViewModelSettings.LoadAsync();

            // Load Home Details
            await ViewModelHome.LoadAsync();

            // Load Notification Details
            await ViewModelNotification.LoadAsync();

            // Go to chat page
            ViewModelApplication.GoToPageAsync(ApplicationPage.Home);
        }

        /// <summary>
        /// Handles what happens when we have successfully registered 
        /// </summary>
        /// <param name="loginResult">The results from the successful registration</param>
        public async Task HandleSuccessfulRegistrationAsync(UserProfileDetailsApiModel loginResult)
        {
            // Store this in the client data store
            await ClientDataStore.SaveLoginCredentialsAsync(loginResult.ToLoginCredentialsDataModel());

            // Go to chat page
            ViewModelApplication.GoToPageAsync(ApplicationPage.Details);
        }

        #endregion
    }
}
