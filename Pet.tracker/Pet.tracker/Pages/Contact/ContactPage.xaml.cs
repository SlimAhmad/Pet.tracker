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
    public partial class ContactPage : BasePage<ContactViewModel>
    {
        public ContactPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public ContactPage(ContactViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}