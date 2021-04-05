
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace  Pet.Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BasePage<HomeViewModel>
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public HomePage(HomeViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }


        #endregion

        public const int AnimationSpeed = 3000;



        private void AddressView_Clicked(object sender, EventArgs e)
        {
            Views.Children.Clear();
            Views.Children.Add(new AddressViewPage());
       

        }



        private void InfoView_Clicked(object sender, EventArgs e)
        {
      
            Views.Children.Clear();
            Views.Children.Add(new InfoViewPage());
           

        }

       
    }
}