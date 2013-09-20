using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Android.OS;
using Android.Gms.Maps;
using Android.App;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Widget;
using Xamarin.ActionbarSherlockBinding.App;
using SherlockActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using ChART.DataAccess.Concrete;
using ChART.Mobile;
using ChART.Domain.Entities;
using Xamarin.Geolocation;
using Android.Support.V4.View;

namespace ChART.Android
{
	[Activity (Label = "ChARTCUU", MainLauncher = true)]
	public class MainActivity : SherlockFragmentActivity, SherlockActionBar.ITabListener
	{
		private ViewPager Pager;
		protected ActionBar ActionBar;

		protected override void OnCreate (Bundle bundle)
		{
			RequestWindowFeature (global::Android.Views.WindowFeatures.NoTitle);
			Window.SetFlags (global::Android.Views.WindowManagerFlags.Fullscreen, global::Android.Views.WindowManagerFlags.Fullscreen);
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

