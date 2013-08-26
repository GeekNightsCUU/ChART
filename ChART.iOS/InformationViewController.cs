using System;
using System.Drawing;
using FlyoutNavigation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;


namespace ChART.iOS
{
	public class InformationViewController : UIViewController
	{
		DialogViewController dialogViewController;
		NavigationViewController navigationViewController;

		public InformationViewController (FlyoutNavigationController navigation, String filename )
		{	
			navigationViewController = new NavigationViewController (navigation);
			dialogViewController = new DialogViewController (JsonElement.FromFile (filename));
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			var bounds = UIScreen.MainScreen.Bounds;
			navigationViewController.View.Frame = new RectangleF (0, 0, bounds.Width, 44);
			dialogViewController.View.Frame = new RectangleF (0, 44, bounds.Width, bounds.Height - 44);
			this.AddChildViewController (navigationViewController);
			this.View.AddSubview (navigationViewController.View);

			this.AddChildViewController (dialogViewController);
			this.View.AddSubview (dialogViewController.View);
		}

		public override void ViewWillLayoutSubviews ()
		{
			base.ViewWillLayoutSubviews ();
			var bounds = UIScreen.MainScreen.Bounds;
			navigationViewController.View.Frame = new RectangleF (0, 0, bounds.Width, 44);
			dialogViewController.View.Frame = new RectangleF (0, 44, bounds.Width, bounds.Height - 44);
		}
	}
}

