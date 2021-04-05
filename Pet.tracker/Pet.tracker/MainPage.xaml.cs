using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;


namespace Pet.Tracker
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = new SettingsViewModel();
        }

        public Dictionary<string, Type> Routes { get; } = new Dictionary<string, Type>();

        public void RegisterRoutes()
        {
      
            Routes.Add("register", typeof(SignUpPage));
            Routes.Add("login", typeof(LoginPage));
            Routes.Add("details", typeof(OwnersDetailsPage));
            Routes.Add("pet", typeof(PetsDetailsPage));
            Routes.Add("transfer", typeof(TransferFormPage));
            Routes.Add("approve", typeof(ApproveFormPage));
            Routes.Add("contact", typeof(ContactPage));
            Routes.Add("find", typeof(FindPetsPage));
            Routes.Add("edit", typeof(SettingsPage));
            Routes.Add("incoming", typeof(IncomingTransferPage));
            Routes.Add("incoming_details", typeof(IncomingDetailsPage));
            Routes.Add("pet_transfer", typeof(TransferPetPage));
            Routes.Add("pet_tdetails", typeof(TransferPetDetailsPage));
            Routes.Add("pet_rdetails", typeof(IncomingPetDetailsPage));
            Routes.Add("missing", typeof(MissingDetailsPage));
            Routes.Add("centers", typeof(VetCentersDetailsPage));
            Routes.Add("settings", typeof(SettingsPage));



            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}
