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
using MonoTouch.MapKit;
using MonoTouch.UIKit;
using FlyoutNavigation;

namespace ChART.iOS
{
	public partial class MapViewController : UIViewController, IStationHolder
	{
		private IStationRepository stationsRepository;
		private IQueryable<Station> stations;
		private Station _station;
		private GCDiscreetNotificationView notificationView;
		private FlyoutNavigationController navigation;
		private MKMapView mapView;

		public Station Station {
			get{
				return this._station;
			}
			set {
				this._station = value;
				notificationView.Hide (true);
				var map = this.mapView;
				map.SetCenterCoordinate (new CLLocationCoordinate2D(_station.Latitude, _station.Longitude), true);
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

			var bounds = UIScreen.MainScreen.Bounds;
			var map = new MKMapView (new RectangleF(0, 44, bounds.Width, bounds.Height));
			this.mapView = map;
			map.MapType = MKMapType.Standard;
			map.ShowsUserLocation = true;
			map.Delegate = new MapViewDelegate ();

			var centerPoint = Station.TroncalRouteCenter;
			var centerCoordinate = new CLLocationCoordinate2D (centerPoint.Y, centerPoint.X);
			map.SetCenterCoordinate (centerCoordinate, true);
			map.Region = MKCoordinateRegion.FromDistance (centerCoordinate, 1250, 2100);

			NavigationBar.TintColor = UIColor.Black;
			var item = new UINavigationItem ("Mapa ViveBus");
			UIBarButtonItem button = new UIBarButtonItem ("Menu", UIBarButtonItemStyle.Bordered, delegate {
				navigation.ToggleMenu();
			});
			item.LeftBarButtonItem = button;
			item.HidesBackButton = true;
			NavigationBar.PushNavigationItem (item, false);

			notificationView = new GCDiscreetNotificationView ("Buscando estación cercana...", 
			                                                  true, GCDNPresentationMode.Bottom, View);
			this.View.AddSubview (map);
			LoadMapInfo ();
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
					mapView.AddAnnotation (new StationPointAnnotation (station.Name, new CLLocationCoordinate2D(station.Latitude,station.Longitude), station));
				}							
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

	class StationPointAnnotation: MKAnnotation
	{
		public Station Station{ get; set; }
		string title;
		CLLocationCoordinate2D coord;

		public StationPointAnnotation( string title, CLLocationCoordinate2D coord, Station station)
		{
			this.title = title;
			this.coord = coord;
			this.Station = station;
		}

		public override string Title{
			get{
				return title;
			}
		}

		public override CLLocationCoordinate2D Coordinate {
			get{
				return coord;
			}
			set{
				WillChangeValue ("coordinate");
				coord = value;
				DidChangeValue ("Coordinate");
			}
		}

	}

	class MapViewDelegate : MKMapViewDelegate
	{
		private readonly string annotationId = "annotationId";

		public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, NSObject annotation)
		{
			if (annotation is MKUserLocation)
				return null; 

			MKAnnotationView pinView = (MKAnnotationView)mapView.DequeueReusableAnnotation (annotationId);
			if (pinView == null)
				pinView = new MKAnnotationView (annotation, annotationId);

			pinView.CanShowCallout = true;
			pinView.RightCalloutAccessoryView = UIButton.FromType (UIButtonType.DetailDisclosure);
			var station = (annotation as StationPointAnnotation).Station;
			var originalImage = UIImage.FromFile ("StationImages/" + station.ImageFilename ());
			var image = resizedImageIcon(originalImage);
			pinView.Image = image;		

			return pinView;
		}

		public override void CalloutAccessoryControlTapped (MKMapView mapView, MKAnnotationView view, UIControl control)
		{

		}


		public UIImage resizedImageIcon(UIImage image)
		{
			SizeF size = new SizeF (25.0f, 25.0f);	
			UIImage newImage = image.Scale (size, 2.0f);
			return newImage;
		}
	}
}

