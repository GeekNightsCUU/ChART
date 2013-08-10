using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ChART.Domain.iOS;

namespace ChART.iOS
{
	public class MainMenuViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("MainMenuViewCell");

		public MainMenuViewCell (MainMenuItem mainMenuItem) : base (UITableViewCellStyle.Default, Key)
		{
			var textColor = UIColor.FromRGB(mainMenuItem.Color()[0],mainMenuItem.Color()[1],mainMenuItem.Color()[2]);
			TextLabel.TextAlignment = UITextAlignment.Center;
			TextLabel.TextColor = textColor;
			TextLabel.BackgroundColor = UIColor.Clear;
			TextLabel.HighlightedTextColor = textColor;
			ContentView.BackgroundColor = UIColor.Black;
			SelectedBackgroundView = DefaultSelectedBackgroundView();
		}


		static UIView DefaultSelectedBackgroundView()
		{
			var backgroundView = new UIView ();
			backgroundView.BackgroundColor = UIColor.White;
			backgroundView.Layer.MasksToBounds = true;
			return backgroundView;
		}
	}
}

