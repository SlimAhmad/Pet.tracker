using Xamarin.Forms.Xaml;

namespace Pet.Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : BasePage<LoginViewModel>
    {


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public LoginPage(LoginViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }





        #endregion

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}
