using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChART.DataAccess.Abstract;
using ChART.DataAccess.Concrete;
using ChART.Domain.Entities;
using ChART.Mobile;
using GCDiscreetNotification;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using FlyoutNavigation;
using Google.Maps;

namespace ChART.iOS
{
	public partial class MapViewController : UIViewController, IStationHolder
	{
		private IStationRepository stationsRepository;
		private IQueryable<Station> stations;
		private Station _station;
		private GCDiscreetNotificationView notificationView;
		private FlyoutNavigationController navigation;
		private MapView mapView;

		public Station Station {
			get{
				return this._station;
			}
			set {
				this._station = value;
				notificationView.Hide (true);
				var map = this.mapView;
				map.Animate(new CLLocationCoordinate2D(_station.Latitude,Station.Longitude));				
				var alert = new UIAlertView("Estación más cercana", _station.Name, null, "Ok", null);
				alert.Show();
			}
		}

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public MapViewController (FlyoutNavigationController navigation)
			: base (UserInterfaceIdiomIsPhone ? "MapViewController_iPhone" : "MapViewController_iPad", null)
		{
			this.navigation = navigation;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			stationsRepository = new WebStationRepository ();

			var centerPoint = Station.TroncalRouteCenter;
			var centerCoordinate = new CLLocationCoordinate2D (centerPoint.Y, centerPoint.X);
			var bounds = UIScreen.MainScreen.Bounds;

			CameraPosition cameraPosition = CameraPosition.FromCamera (centerCoordinate, 14.0f);
			var map = MapView.FromCamera (new RectangleF (0, 44, bounds.Width, bounds.Height - 60), cameraPosition);
			map.MyLocationEnabled = true;
			map.MapType = MapViewType.Normal;

			this.mapView = map;

			NavigationBar.TintColor = UIColor.Black;
			var item = new UINavigationItem ("Transporte Público Chihuahua");
			UIBarButtonItem button = new UIBarButtonItem (MainViewController.ResizedImageIcon(UIImage.FromFile("menu.png")), UIBarButtonItemStyle.Bordered, delegate {
				navigation.ToggleMenu();
			});
			UIBarButtonItem closestStationButton = new UIBarButtonItem ("Cercana", UIBarButtonItemStyle.Bordered, delegate {
				FindClosestStation();
			});

			item.LeftBarButtonItem = button;
			item.HidesBackButton = true;
			item.RightBarButtonItem = closestStationButton;
			NavigationBar.PushNavigationItem (item, false);

			notificationView = new GCDiscreetNotificationView ("Buscando estación cercana...", 
			                                                  true, GCDNPresentationMode.Bottom, mapView);
			this.View.AddSubview (map);
			LoadMapInfo ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			mapView.StartRendering ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			mapView.StopRendering ();
			base.ViewWillDisappear (animated);
		}

		public void LoadMapInfo()
		{
			if (stations == null) {
				ThreadPool.QueueUserWorkItem (o => {
					SetStationsFromRepository ();
					FindClosestStation();
				});
			} else {
				FindClosestStation ();
			}
		}

		private void SetStationsFromRepository()
		{
			stations = stationsRepository.Stations;
			InvokeOnMainThread (() => {
				foreach (var station in stations) {
					var stationPosition = new CLLocationCoordinate2D(station.Latitude, station.Longitude);
					Marker marker = Marker.FromPosition(stationPosition);
					marker.Title = station.Name;
					var stationIcon = UIImage.FromFile ("StationImages/" + station.ImageFilename ());
					marker.Icon = MainViewController.ResizedImageIcon(stationIcon);
					marker.Map = mapView;
				}	
				var path = new MutablePath();
				foreach(var point in Station.TroncalRoutePath)
				{
					path.AddCoordinate(new CLLocationCoordinate2D(point.Y, point.X));
				}
				var poliline = Polyline.FromPath(path);
				poliline.StrokeColor = UIColor.Red;
				poliline.StrokeWidth = 4.0f;
				poliline.Map = mapView;
			});
		}

		void FindClosestStation ()
		{
			InvokeOnMainThread (() => {
				TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext ();
				notificationView.Show (true);
				StationGeolocationUtil.CurrentClosestStation (stations, this, scheduler);
			});
		}
	}
}

