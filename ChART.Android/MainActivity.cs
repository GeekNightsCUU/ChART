using System;
using Android.OS;
using Android.App;
using Xamarin.ActionbarSherlockBinding.App;
using SherlockActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using Android.Support.V4.View;
using Android.Content.PM;

namespace ChART.Android
{
	[Activity (Label = "ChARTCUU", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : SherlockFragmentActivity, SherlockActionBar.ITabListener
	{
		private ViewPager Pager;
		protected ActionBar ActionBar;

		protected override void OnCreate (Bundle bundle)
		{
			SetTheme (Resource.Style.Theme_Sherlock_Light);
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MainNavigation);
			SupportActionBar.NavigationMode = SherlockActionBar.NavigationModeTabs;
			SupportActionBar.SetDisplayShowTitleEnabled (false);
			SupportActionBar.SetDisplayShowHomeEnabled (false);
			Pager = FindViewById<ViewPager> (Resource.Id.pager);
			var fm = SupportFragmentManager;
			Pager.PageSelected += (object sender, ViewPager.PageSelectedEventArgs e) => {
				SupportActionBar.SetSelectedNavigationItem(e.Position);
			};

			Pager.Adapter =  new ViewPagerAdapter (fm);

			var tab = SupportActionBar.NewTab ().SetText ("Mapa").SetTabListener (this);
			SupportActionBar.AddTab (tab);
			tab = SupportActionBar.NewTab ().SetText ("FAQS").SetTabListener (this);
			SupportActionBar.AddTab (tab);
			tab = SupportActionBar.NewTab ().SetText ("Acerca de").SetTabListener (this);
			SupportActionBar.AddTab (tab);
		}

		public void OnTabSelected (SherlockActionBar.Tab tab, FragmentTransaction transaction)
		{
			Pager.SetCurrentItem (tab.Position, true);
		}

		public void OnTabReselected (SherlockActionBar.Tab tab, FragmentTransaction transaction)
		{
		}
						
		public void OnTabUnselected (SherlockActionBar.Tab tab, FragmentTransaction transaction)
		{
		}
	}
}

