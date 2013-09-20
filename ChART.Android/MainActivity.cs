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

namespace ChART.Android
{
	[Activity (Label = "ChARTCUU", MainLauncher = true)]
	public class MainActivity : SherlockFragmentActivity, SherlockActionBar.ITabListener, IStationHolder
	{
		private readonly string[] tabs = {"Mapa", "FAQS", "Acerca de"};
		private GoogleMap _map;
		private SupportMapFragment _mapFragment;
		private WebStationRepository stationRepository;
		private Button closestStationButton;
		private IQueryable<Station> stations;
		private Station _station;
		private ProgressDialog progressDialog;

		protected override void OnCreate (Bundle bundle)
		{
			RequestWindowFeature (global::Android.Views.WindowFeatures.NoTitle);
			Window.SetFlags (global::Android.Views.WindowManagerFlags.Fullscreen, global::Android.Views.WindowManagerFlags.Fullscreen);
			SetTheme (Resource.Style.Theme_Sherlock_Light);
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MainNavigation);
			stationRepository = new WebStationRepository ();
			closestStationButton = FindViewById<Button> (Resource.Id.closestStation);
			closestStationButton.Enabled = false;
			closestStationButton.Click += delegate {
				FindClosestStation();
			};
			InitMapFragment();

			SupportActionBar.NavigationMode = SherlockActionBar.NavigationModeTabs;
			foreach (string tabTitle in tabs) {
				var tab = SupportActionBar.NewTab ();
				tab.SetText (tabTitle);
				tab.SetTabListener (this);
				SupportActionBar.AddTab (tab);
			}
			SupportActionBar.SetDisplayShowTitleEnabled (false);
			SupportActionBar.SetDisplayShowHomeEnabled (false);
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

		public Station Station {
			get{
				return this._station;
			}
			set {
				this._station = value;
				progressDialog.Dismiss ();
				var centerPoint = new LatLng (_station.Latitude, _station.Longitude);
				_map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(centerPoint,15.0f));
				new AlertDialog.Builder (this).SetTitle ("Estación más cercana").SetMessage (_station.Name).SetNeutralButton ("Ok", delegate {}).Show ();
			}
		}

		private void FindClosestStation ()
		{
			RunOnUiThread (() => {
				TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext ();
				progressDialog = new ProgressDialog(this);
				progressDialog.SetMessage("Buscando estación cercana...");
				progressDialog.Show();
				progressDialog.SetCanceledOnTouchOutside(false);
				progressDialog.SetCancelable(false);
				StationGeolocationUtil.CurrentClosestStation (stations, this, scheduler, new Geolocator(this));
			});
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
						_map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(centerPoint,15.0f));
						_map.MyLocationEnabled = true;
					});
					stations = stationRepository.Stations;
					RunOnUiThread (delegate{
						foreach(var station in stations){
							var location = new LatLng(station.Latitude, station.Longitude);
							var stationImageId = Resources.GetIdentifier( station.ImageFilename().ToLower(), "drawable", PackageName);
							var originalStationBitmap = BitmapFactory.DecodeResource(Resources, stationImageId);
							var scale = Resources.DisplayMetrics.Density;
							var pixels = 35;
							var stationBitmap = Bitmap.CreateScaledBitmap(originalStationBitmap, (int) (pixels * scale + 0.5f), (int) (pixels * scale + 0.5f), false);
							MarkerOptions markerOptions = new MarkerOptions()
								.SetPosition(location)
								.InvokeIcon(BitmapDescriptorFactory.FromBitmap(stationBitmap))
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
						closestStationButton.Enabled = true;
					});
				}
			}
		}
	}
}

