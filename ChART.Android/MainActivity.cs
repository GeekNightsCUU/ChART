using System;
using Android.OS;
using Android.Gms.Maps;
using Android.App;
using Xamarin.ActionbarSherlockBinding.App;
using SherlockActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using ChART.Domain.Entities;
using Android.Gms.Maps.Model;
using System.Threading;
using ChART.DataAccess.Concrete;
using Android.Graphics;

namespace ChART.Android
{
	[Activity (Label = "ChARTCUU", MainLauncher = true)]
	public class MainActivity : SherlockFragmentActivity, SherlockActionBar.ITabListener
	{
		private readonly string[] tabs = {"Mapa", "FAQS", "Acerca de"};
		private GoogleMap _map;
		private SupportMapFragment _mapFragment;
		private WebStationRepository stationRepository;

		protected override void OnCreate (Bundle bundle)
		{
			SetTheme (Resource.Style.Theme_Sherlock);
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MainNavigation);
			stationRepository = new WebStationRepository ();
			InitMapFragment();

			SupportActionBar.NavigationMode = SherlockActionBar.NavigationModeTabs;
			foreach (string tabTitle in tabs) {
				var tab = SupportActionBar.NewTab ();
				tab.SetText (tabTitle);
				tab.SetTabListener (this);
				SupportActionBar.AddTab (tab);
			}
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			ThreadPool.QueueUserWorkItem (o => {
				SetupMapIfNeeded ();
			});
		}

		public void OnTabReselected (SherlockActionBar.Tab tab, FragmentTransaction transaction)
		{
		}

		public void OnTabSelected (SherlockActionBar.Tab tab, FragmentTransaction transaction)
		{
		}

		
		public void OnTabUnselected (SherlockActionBar.Tab tab, FragmentTransaction transaction)
		{
		}

		private void InitMapFragment()
		{
			_mapFragment = SupportFragmentManager.FindFragmentByTag("map") as SupportMapFragment;
			if (_mapFragment == null)
			{
				GoogleMapOptions mapOptions = new GoogleMapOptions()
					.InvokeMapType(GoogleMap.MapTypeNormal)
						.InvokeZoomControlsEnabled(false)
						.InvokeCompassEnabled(true);
				var fm = SupportFragmentManager;
				FragmentTransaction fragTx = fm.BeginTransaction ();
				_mapFragment = SupportMapFragment.NewInstance(mapOptions);
				fragTx.Add(Resource.Id.map, _mapFragment, "map");
				fragTx.Commit();
				fm.ExecutePendingTransactions ();
			}
		}

		private void SetupMapIfNeeded()
		{
			if (_map == null)
			{
				_map = _mapFragment.Map;
				if (_map != null)
				{
					var centerPoint = new LatLng (Station.TroncalRouteCenter.Y, Station.TroncalRouteCenter.X);
					RunOnUiThread (delegate{
						_map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(centerPoint,14.0f));
						_map.MyLocationEnabled = true;
					});
					var stations = stationRepository.Stations;
					RunOnUiThread (delegate{
						foreach(var station in stations){
							var location = new LatLng(station.Latitude, station.Longitude);
							MarkerOptions markerOptions = new MarkerOptions()
								.SetPosition(location)
								.SetTitle(station.Name);
							_map.AddMarker(markerOptions);
						}
						var polylineOptions = new PolylineOptions();
						polylineOptions.InvokeWidth(5.0f);
						polylineOptions.InvokeColor(Color.Black);
						foreach(var point in Station.TroncalRoutePath){
							polylineOptions.Add(new LatLng(point.Y, point.X));
						}
						_map.AddPolyline(polylineOptions);
					});

				}
			}
		}
	}
}

