using Pet.Tracker.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Pet.Tracker
{
    /// <summary>
    /// Extension method for 
    /// </summary>
    public static class PageHelper
    {
      
            /// <summary>
            /// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the desired page
            /// </summary>
            /// <param name="page"></param>
            /// <param name="viewModel"></param>
            /// <returns></returns>
            public static Page ToBasePage(this ApplicationPage page, object viewModel = null)
            {
                // Find the appropriate page
                switch (page)
                {
                    case ApplicationPage.Login:
                        return new LoginPage(viewModel as LoginViewModel);

                    case ApplicationPage.Home:
                    return new MainPage();
                    
                    case ApplicationPage.Register:
                    return new SignUpPage(viewModel as RegisterViewModel);

                case ApplicationPage.Details:
                    return new OwnersDetailsPage(viewModel as OwnerDetailsViewModel);

                case ApplicationPage.Pet:
                    return new PetsDetailsPage(viewModel as PetsDetailsViewModel);

                default:
                        Debugger.Break();
                        return null;
                }
            }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find application page that matches the base page
            if (page is HomePage)
                return ApplicationPage.Home;

            if (page is LoginPage)
                return ApplicationPage.Login;

            if (page is SignUpPage)
                return ApplicationPage.Register;

            // Alert developer of issue
            Debugger.Break();
            return default(ApplicationPage);
        }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string ToRoutePage(this ApplicationPage page)
        {
            // Find the appropriate page
            switch (page)
            {
                case ApplicationPage.Login:
                    return "//login";
                case ApplicationPage.Register:
                    return "//login/register";
                case ApplicationPage.Details:
                    return "details";
                case ApplicationPage.Pet:
                    return "pet";
                case ApplicationPage.Logout:
                    return "///boards/loading/login";
                case ApplicationPage.Home:
                    return "///main/home";
                case ApplicationPage.Contact:
                    return "contact";
                case ApplicationPage.Approve:
                    return "approve";
                case ApplicationPage.Transfer:
                    return "transfer";
                case ApplicationPage.Incoming:
                    return "incoming";
                case ApplicationPage.IDetails:
                    return "incoming_details";
                case ApplicationPage.PTransfer:
                    return "pet_transfer";
                case ApplicationPage.Missing:
                    return "missing";
                case ApplicationPage.Centers:
                    return "centers";
                case ApplicationPage.Settings:
                    return "settings";
                case ApplicationPage.Edit:
                    return "edit";
                case ApplicationPage.Find:
                    return "find";
                case ApplicationPage.PTDetails:
                    return "pet_tdetails";
                case ApplicationPage.RDetails:
                    return "pet_rdetails";


                default:
                    Debugger.Break();
                    return null;
            }
        }
    }
}
