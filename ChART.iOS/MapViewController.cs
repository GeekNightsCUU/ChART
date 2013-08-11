using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using ChART.DataAccess.Abstract;
using ChART.DataAccess.Concrete;
using MonoTouch.CoreLocation;
using System.Threading;
using ChART.Domain.Entities;

namespace ChART.iOS
{
	public partial class MapViewController : UIViewController
	{
		private IStationRepository stationsRepository;
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public MapViewController ()
			: base (UserInterfaceIdiomIsPhone ? "MapViewController_iPhone" : "MapViewController_iPad", null)
		{
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
			var map = new MKMapView(UIScreen.MainScreen.Bounds);
			map.MapType = MKMapType.Standard;
			map.ShowsUserLocation = true;
			map.Delegate = new MapViewDelegate ();

			var centerPoint = Station.TroncalRouteCenter;
			var centerCoordinate = new CLLocationCoordinate2D (centerPoint.Y, centerPoint.X);
			map.SetCenterCoordinate (centerCoordinate, true);
			map.Region = MKCoordinateRegion.FromDistance (centerCoordinate, 3000, 5000);
			this.View = map;
			ThreadPool.QueueUserWorkItem (o => SetStationsFromRepository ());
		}

		private void SetStationsFromRepository()
		{
			var stations = stationsRepository.Stations;
			InvokeOnMainThread (() => {
				var map = this.View as MKMapView;
				foreach (var station in stations) {
					map.AddAnnotation (new StationPointAnnotation () {
						Title = station.Name,
						Coordinate = new CLLocationCoordinate2D(station.Latitude,station.Longitude),
						Station = station,
					});
				}
			});
		}
	}

	class StationPointAnnotation: MKPointAnnotation
	{
		public Station Station{ get; set; }
		public MKPinAnnotationColor GetAnnotationColor()
		{
			if(this.Title.Contains("Terminal"))
			{
				return MKPinAnnotationColor.Green;
			}
			else
			{
				return MKPinAnnotationColor.Red;
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

			MKAnnotationView pinView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation (annotationId);
			if (pinView == null)
				pinView = new MKPinAnnotationView (annotation, annotationId);

			((MKPinAnnotationView)pinView).PinColor = (annotation as StationPointAnnotation).GetAnnotationColor();
			pinView.CanShowCallout = true;
			pinView.RightCalloutAccessoryView = UIButton.FromType (UIButtonType.DetailDisclosure);
			var station = (annotation as StationPointAnnotation).Station;
			var originalImage = UIImage.FromFile ("StationImages/" + station.ImageFilename ());
			var image = resizedImageIcon(originalImage);
			pinView.LeftCalloutAccessoryView = new UIImageView (image);

			return pinView;
		}


		public UIImage resizedImageIcon(UIImage image)
		{
			SizeF size = new SizeF (35.0f, 35.0f);					
			UIImage newImage = image.Scale (size);
			return newImage;
		}
	}
}

