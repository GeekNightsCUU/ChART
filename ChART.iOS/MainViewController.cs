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
		public static UIColor MainBackgroundColor = UIColor.FromRGB(30,30,30);
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
						new StringElement ("FAQS"),
						new StringElement ("Acerca de"),
					}
				},
			};
			navigation.ViewControllers = new UIViewController[] {
				new MapViewController(navigation),
				new InformationViewController(navigation, "faqs.json"),
				new InformationViewController(navigation, "about.json"),
			};
			navigation.View.Frame = UIScreen.MainScreen.Bounds;
			navigation.HideShadow = false;
			navigation.NavigationTableView.BackgroundColor = MainBackgroundColor;
			navigation.NavigationTableView.SeparatorColor = UIColor.DarkGray;
			navigation.NavigationTableView.Delegate = new NavigationTableViewDelegate ();
			View.AddSubview (navigation.View);
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public static UIImage ResizedImageIcon(UIImage image)
		{
			SizeF size = new SizeF (25.0f, 25.0f);	
			UIImage newImage = image.Scale (size, 2.0f);
			return newImage;
		}
	}

	class NavigationTableViewDelegate : UITableViewDelegate
	{
		public override void WillDisplay (UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			cell.TextLabel.TextColor = UIColor.White;
			cell.TextLabel.HighlightedTextColor = UIColor.DarkGray;
		}
	}
}

