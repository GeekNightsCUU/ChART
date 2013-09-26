using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Google.Maps;
using Style;

namespace ChART.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
		private const string GoogleMapsAPI = "AIzaSyD151AuXVZ421ThiGi_F3VGzoBwfFkKXmk";

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			NativeCSS.StyleWithCSS (".menu-table{color:#0b93ff}");
			MapServices.ProvideAPIKey (GoogleMapsAPI);                        
			window = new UIWindow(UIScreen.MainScreen.Bounds);
			var rootNavigationController = new MainViewController (); 
			window.RootViewController = rootNavigationController;            
            window.MakeKeyAndVisible();

            return true;
        }
    }
}