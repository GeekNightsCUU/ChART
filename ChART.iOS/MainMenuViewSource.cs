using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ChART.iOS
{
	public class MainMenuViewSource : UITableViewSource
	{
		private string[] MenuItems = {"Mapa ViveBus","Estación más Cercana","FAQS"};
		private MainMenuViewController viewController;
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
			return 3;
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 132.0f;
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
			var cell = tableView.DequeueReusableCell (MainMenuViewCell.Key) as MainMenuViewCell;
			if (cell == null)
				cell = new MainMenuViewCell ();

			cell.TextLabel.Text = MenuItems [indexPath.Row];
			
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

