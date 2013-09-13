using System;
using Android.OS;
using Android.Widget;
using Android.App;
using Xamarin.ActionbarSherlockBinding.App;
using SherlockActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace ChART.Android
{
	[Activity (Label = "ChARTCUU", MainLauncher = true)]
	public class MainActivity : SherlockActivity, SherlockActionBar.ITabListener
	{
		private TextView mSelected;
		private readonly string[] tabs = {"Mapa", "FAQS", "Acerca de"};
		protected override void OnCreate (Bundle bundle)
		{
			SetTheme (Resource.Style.Theme_Sherlock);
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MainNavigation);
			mSelected = FindViewById<TextView> (Resource.Id.text);
			SupportActionBar.NavigationMode = SherlockActionBar.NavigationModeTabs;

			foreach (string tabTitle in tabs) {
				var tab = SupportActionBar.NewTab ();
				tab.SetText (tabTitle);
				tab.SetTabListener (this);
				SupportActionBar.AddTab (tab);
			}
		}

		public void OnTabReselected (SherlockActionBar.Tab tab, FragmentTransaction transaction)
		{
		}

		public void OnTabSelected (SherlockActionBar.Tab tab, FragmentTransaction transaction)
		{
			mSelected.Text = "Selected: " + tab.Text;
		}

		
		public void OnTabUnselected (SherlockActionBar.Tab tab, FragmentTransaction transaction)
		{
		}
	}
}

