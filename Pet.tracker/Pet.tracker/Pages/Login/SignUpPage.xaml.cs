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
    public partial class SignUpPage : BasePage<RegisterViewModel>
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SignUpPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public SignUpPage(RegisterViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }


        #endregion
    }
}