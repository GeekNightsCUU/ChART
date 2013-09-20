using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.ActionbarSherlockBinding.App;
using Android.Gms.Maps;
using ChART.DataAccess.Concrete;
using ChART.Domain.Entities;
using ChART.Mobile;
using Android.Gms.Maps.Model;
using System.Threading;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Support.V4.App;
using Xamarin.Geolocation;

namespace ChART.Android
{
	public class MapsFragment : SherlockFragment, IStationHolder
	{
		private GoogleMap _map;
		private SupportMapFragment _mapFragment;
		private WebStationRepository stationRepository;
		private Button closestStationButton;
		private IQueryable<Station> stations;
		private Station _station;
		private ProgressDialog progressDialog;
		private View view;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);		
		}

		public override void OnResume ()
		{
			base.OnResume ();
			ThreadPool.QueueUserWorkItem (o => {
				SetupMapIfNeeded ();
			});
			closestStationButton.Enabled = true;
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);
			InitMapFragment();
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			view = inflater.Inflate (Resource.Layout.MapNavigation, container, false);
			stationRepository = new WebStationRepository ();
			closestStationButton = view.FindViewById<Button> (Resource.Id.closestStation);
			closestStationButton.Enabled = false;
			closestStationButton.Click += delegate {
				FindClosestStation();
			};
			return view;
		}

		public override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);
			UserVisibleHint = true;
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
				new AlertDialog.Builder (Activity).SetTitle ("Estación más cercana").SetMessage (_station.Name).SetNeutralButton ("Ok", delegate {}).Show ();
			}
		}

		private void FindClosestStation ()
		{
			Activity.RunOnUiThread (() => {
				TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext ();
				progressDialog = new ProgressDialog(Activity);
				progressDialog.SetMessage("Buscando estación cercana...");
				progressDialog.Show();
				progressDialog.SetCanceledOnTouchOutside(false);
				progressDialog.SetCancelable(false);
				StationGeolocationUtil.CurrentClosestStation (stations, this, scheduler, new Geolocator(Activity));
			});
		}

		private void InitMapFragment()
		{
			_mapFragment = ChildFragmentManager.FindFragmentByTag("map") as SupportMapFragment;
			if (_mapFragment == null)
			{
				GoogleMapOptions mapOptions = new GoogleMapOptions()
					.InvokeMapType(GoogleMap.MapTypeNormal)
					.InvokeZoomControlsEnabled(false)
					.InvokeCompassEnabled(true);
				var fm = ChildFragmentManager;
				FragmentTransaction fragTx = fm.BeginTransaction ();
				_mapFragment = SupportMapFragment.NewInstance(mapOptions);
				fragTx.Add(Resource.Id.map, _mapFragment, "map");
				fragTx.Commit();
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
					Activity.RunOnUiThread (delegate{
						_map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(centerPoint,15.0f));
						_map.MyLocationEnabled = true;
					});
					stations = stationRepository.Stations;
					Activity.RunOnUiThread (delegate{
						foreach(var station in stations){
							var location = new LatLng(station.Latitude, station.Longitude);
							var stationImageId = Resources.GetIdentifier( station.ImageFilename().ToLower(), "drawable", Activity.PackageName);
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
					});
				}
			}
		}
	}
}

