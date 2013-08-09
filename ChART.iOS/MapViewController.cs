using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using ChART.DataAccess.Abstract;
using ChART.DataAccess.Concrete;
using MonoTouch.CoreLocation;
using System.Threading;

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
			map.ShowsUserLocation = true;
			map.Delegate = new MapViewDelegate ();
			map.SetCenterCoordinate (new CLLocationCoordinate2D (28.3807, -106.0520), true);
			this.View = map;
			ThreadPool.QueueUserWorkItem (o => SetStationsFromRepository ());
		}

		private void SetStationsFromRepository()
		{
			var stations = stationsRepository.Stations;
			InvokeOnMainThread (() => {
				var map = this.View as MKMapView;
				foreach (var station in stations) {
					map.AddAnnotation (new MKPointAnnotation () {
						Title = station.Name,
						Coordinate = new CLLocationCoordinate2D(station.Latitude,station.Longitude),
					});
				}
			});
		}
	}

	class MapViewDelegate : MKMapViewDelegate
	{

	}
}

