using System;
using System.Collections.Generic;
using System.Linq;
using ChART.Domain.Entities;
using Xamarin.Geolocation;
using System.Drawing;

namespace ChART.DataAccess.Concrete
{
	public class StationGeolocationUtil
	{
		public Station CurrentClosestStation(IQueryable<Station> stations)
		{
			var locator = new Geolocator { DesiredAccuracy = 50 };
			var task = locator.GetPositionAsync (10000);
			task.Wait ();
			var currentStation = new Station{Latitude = task.Result.Latitude, Longitude = task.Result.Longitude};
			return currentStation;
		}
	}
}

