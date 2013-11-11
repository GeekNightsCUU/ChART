using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using FlyoutNavigation;
using MonoTouch.Dialog;
using Google.Maps;
using Style;

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
						new StringElement ("FAQS"),
						new StringElement ("Acerca de"),
					}
				},
			};
			navigation.ViewControllers = new UIViewController[] {
				new MapViewController(navigation),
				new InformationViewController(navigation, "Preguntas Frecuentes", "faqs.json"),
				new InformationViewController(navigation,"Acerca de", "about.json"),
			};
			navigation.View.Frame = UIScreen.MainScreen.Bounds;
			navigation.HideShadow = true;
			navigation.ShouldReceiveTouch += (recognizer, touch) => {
				if(touch.View.Superview.GetType() == typeof(MapView)){
					return false;
				}else{

					return true;
				}
			};
			View.AddSubview (navigation.View);
			navigation.NavigationTableView.AddCSSClass ("menu-table");
		}

		public static UIImage ResizedImageIcon(UIImage image)
		{
			SizeF size = new SizeF (25.0f, 25.0f);	
			UIImage newImage = image.Scale (size, 2.0f);
			return newImage;
		}

		public static void VisitCommunity(object obj){
			UIApplication.SharedApplication.OpenUrl (new NSUrl ("https://plus.google.com/communities/113864651382277557583"));
		}
	}

	class NavigationTableViewDelegate : UITableViewDelegate
	{
		public override void WillDisplay (UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			//			cell.TextLabel.TextColor = UIColor.White;
			//			cell.TextLabel.HighlightedTextColor = UIColor.DarkGray;
		}
	}
}

