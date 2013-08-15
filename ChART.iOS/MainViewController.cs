using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using FlyoutNavigation;
using MonoTouch.Dialog;

namespace ChART.iOS
{
	public partial class MainViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public MainViewController ()
			: base (UserInterfaceIdiomIsPhone ? "MainViewController_iPhone" : "MainViewController_iPad", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			var navigation = new FlyoutNavigationController () {
				NavigationRoot = new RootElement("ViveBus"){
					new Section("Menu"){
						new StringElement ("Mapa"),
						new StringElement ("Estación más cercana"),
						new StringElement ("FAQS"),
					}
				},
			};
			navigation.ViewControllers = new [] {
//				new UIViewController { View = new UILabel { Text = "Mapa" } },
				new MapViewController(navigation),
				new UIViewController { View = new UILabel { Text = "Estación Cercana" } },
				new UIViewController { View = new UILabel { Text = "FAQS" } },
			};
			navigation.View.Frame = UIScreen.MainScreen.Bounds;
			navigation.HideShadow = false;
			View.AddSubview (navigation.View);
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

