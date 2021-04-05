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
    public partial class TransferPetPage : BasePage<TransferPetViewModel>
    {
        public TransferPetPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public TransferPetPage(TransferPetViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}