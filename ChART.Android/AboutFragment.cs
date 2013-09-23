//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using Xamarin.ActionbarSherlockBinding.App;

namespace ChART.Android
{
	public class AboutFragment : SherlockFragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.AboutNavigation, container, false);
			var facebook = view.FindViewById<ImageView> (Resource.Id.imageView_facebook);
			facebook.Click += (object sender, System.EventArgs e) => {
				var uri = Uri.Parse ("http://www.facebook.com/geeknightscuu");
				var intent = new Intent (Intent.ActionView, uri);
				StartActivity (intent);     
			};
			var twitter = view.FindViewById<ImageView> (Resource.Id.imageView_twitter);
			twitter.Click += (object sender, System.EventArgs e) => {
				var uri = Uri.Parse ("http://www.twitter.com/geeknightscuu");
				var intent = new Intent (Intent.ActionView, uri);
				StartActivity (intent);     
			};
			var plus = view.FindViewById<ImageView> (Resource.Id.imageView_plus);
			plus.Click += (object sender, System.EventArgs e) => {
				var uri = Uri.Parse ("https://plus.google.com/communities/113864651382277557583");
				var intent = new Intent (Intent.ActionView, uri);
				StartActivity (intent);     
			};
			return view;
		}

		public override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);
			UserVisibleHint = true;
		}
	}
}

