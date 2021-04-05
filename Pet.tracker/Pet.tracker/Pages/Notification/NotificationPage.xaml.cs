using Xamarin.Forms.Xaml;

namespace  Pet.Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage : BasePage<MissingPetsViewModel>
    {
        public NotificationPage()
        {
            InitializeComponent();

            
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public NotificationPage(MissingPetsViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}