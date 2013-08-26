using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using FlyoutNavigation;
using MonoTouch.Dialog;

namespace ChART.iOS
{
	public partial class NavigationViewController : UIViewController
	{
		private FlyoutNavigationController navigation;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public NavigationViewController (FlyoutNavigationController navigation)
			: base (UserInterfaceIdiomIsPhone ? "NavigationViewController_iPhone" : "NavigationViewController_iPad", null)
		{
			this.navigation = navigation;
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
			NavigationBar.TintColor = UIColor.Black;
			var item = new UINavigationItem ("Transporte Público Chihuahua");
			UIBarButtonItem button = new UIBarButtonItem (MainViewController.ResizedImageIcon(UIImage.FromFile("menu.png")), UIBarButtonItemStyle.Bordered, delegate {
				navigation.ToggleMenu();
			});
			item.LeftBarButtonItem = button;
			item.HidesBackButton = true;
			NavigationBar.PushNavigationItem (item, false);
		}
	}
}

