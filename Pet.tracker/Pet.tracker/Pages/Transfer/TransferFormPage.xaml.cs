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
    public partial class TransferFormPage : BasePage<TransferViewModel>
    {
        public TransferFormPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public TransferFormPage(TransferViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }

}