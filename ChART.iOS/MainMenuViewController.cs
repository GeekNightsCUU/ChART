using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ChART.iOS
{
	public class MainMenuViewController : UITableViewController
	{
		private MapViewController mapViewController;
		public MainMenuViewController () : base (UITableViewStyle.Plain)
		{
			Title = "Transporte Público Chihuahua";
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
			// Register the TableView's data source
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
			TableView.SeparatorColor = UIColor.Black;
			TableView.BackgroundColor = UIColor.Black;
			TableView.Source = new MainMenuViewSource (this);
		}

		public void ShowMapViewController ()
		{
			if (mapViewController == null) {
				mapViewController = new MapViewController ();
			}

			this.NavigationController.PushViewController (mapViewController, true);
			mapViewController.LoadMapInfo ();
		}
	}
}

