using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using static Pet.Tracker.DI;

namespace Pet.Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : BasePage
    {


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoadingPage()
        {
            InitializeComponent();
          
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            ViewModelApplication.Init();
        }


 


        #endregion



    }
}