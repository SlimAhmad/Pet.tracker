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
    public partial class IncomingDetailsPage : BasePage<RecieveViewModel>
    {
        public IncomingDetailsPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public IncomingDetailsPage(RecieveViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}