using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace  Pet.Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : BasePage<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void OwnerInfoPage_Clicked(object sender, EventArgs e)
        {
            Views.Children.Clear();
            Views.Children.Add(new OwnerInfoViewPage());
        }

        private void PetInfoPage_Clicked(object sender, EventArgs e)
        {
            Views.Children.Clear();
            Views.Children.Add(new PetInfoViewPage());
        }
    }
}