using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ChART.Domain.iOS;

namespace ChART.iOS
{
	public class MainMenuViewSource : UITableViewSource
	{
		private MainMenuViewController viewController;
		private bool IsPhone()
		{
			return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
		}

		public MainMenuViewSource (MainMenuViewController viewController)
		{
			this.viewController = viewController;
		}

		public override int NumberOfSections (UITableView tableView)
		{
			// TODO: return the actual number of sections
			return 1;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			// TODO: return the actual number of items in the section
			return 7;
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			if (IsPhone ()) {
				return indexPath.Row % 2 == 0? 90.0f: 10.0f ;
			} else {
				return indexPath.Row % 2 == 0? 220.0f: 20.0f ;
			}
		}

		public override string TitleForHeader (UITableView tableView, int section)
		{
			return null;
		}

		public override string TitleForFooter (UITableView tableView, int section)
		{
			return "© Geek Nights CUU";
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var menuItem = MainMenuItem.None.FromOrder (indexPath.Row/2);
			var cell = tableView.DequeueReusableCell (MainMenuViewCell.Key) as MainMenuViewCell;
			if (cell == null) {
				if (indexPath.Row % 2 == 0) {
					cell = new MainMenuViewCell (menuItem);
					cell.TextLabel.Text = menuItem.Title ();			
				} else {
					cell = new MainMenuViewCell ();
				}
			}
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Row == 0) {
				viewController.ShowMapViewController ();
			}
		}
	}
}

