using Foundation;
using System;
using System.IO;
using UIKit;

namespace Pet.Tracker.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        /// <summary>
        /// The sq-lite Database path
        /// </summary>
        public string DbPath { get; set; }

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            SQLitePCL.Batteries_V2.Init();

            var libPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library");
            if (!Directory.Exists(libPath))
            {
                Directory.CreateDirectory(libPath);
            }

            DbPath = Path.Combine(libPath, "PetTracker.db");

            LoadApplication(new App(DbPath));

            return base.FinishedLaunching(app, options);
        }
    }
}
