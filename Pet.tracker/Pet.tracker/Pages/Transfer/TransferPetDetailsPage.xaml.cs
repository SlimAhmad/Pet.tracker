using Xamarin.Forms.Xaml;

namespace Pet.Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferPetDetailsPage : BasePage<TransferPetsDetailsViewModel>
    {
        public TransferPetDetailsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public TransferPetDetailsPage(TransferPetsDetailsViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

    }
}