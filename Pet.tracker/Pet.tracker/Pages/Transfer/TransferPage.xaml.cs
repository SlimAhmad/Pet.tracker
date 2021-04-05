using Xamarin.Forms.Xaml;

namespace  Pet.Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferPage : BasePage<TransferViewModel>
    {
        public TransferPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public TransferPage(TransferViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}