using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ChART.iOS
{
	public class MainMenuViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("MainMenuViewCell");

		public MainMenuViewCell () : base (UITableViewCellStyle.Value1, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
		}
	}
}

