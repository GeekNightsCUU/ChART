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
		private string title;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public NavigationViewController (FlyoutNavigationController navigation, String title)
			: base (UserInterfaceIdiomIsPhone ? "NavigationViewController_iPhone" : "NavigationViewController_iPad", null)
		{
			this.navigation = navigation;
			this.title = title;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			if (float.Parse (UIDevice.CurrentDevice.SystemVersion) >= MainViewController.CurrentVersion) {
				NavigationBar.Frame = new RectangleF (NavigationBar.Frame.X, NavigationBar.Frame.Y, NavigationBar.Frame.Width, 64.0f);
			}
			var item = new UINavigationItem (this.title);
			UIBarButtonItem button = new UIBarButtonItem (MainViewController.ResizedImageIcon(UIImage.FromFile("menu.png")), UIBarButtonItemStyle.Bordered, delegate {
				navigation.ToggleMenu();
			});
			item.LeftBarButtonItem = button;
			item.HidesBackButton = true;
			NavigationBar.PushNavigationItem (item, false);
		}
	}
}

