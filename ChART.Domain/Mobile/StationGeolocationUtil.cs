using System;
using System.Collections.Generic;
using System.Linq;
using ChART.Domain.Entities;
using Xamarin.Geolocation;
using System.Drawing;
using ChART.Mobile;
using System.Threading.Tasks;

namespace ChART.Mobile
{
	public class StationGeolocationUtil
	{
		public static void CurrentClosestStation(IQueryable<Station> stations, IStationHolder holder, TaskScheduler scheduler)
		{
			var locator = new Geolocator { DesiredAccuracy = 50 };
			locator.GetPositionAsync (10000).ContinueWith (t => {
				var currentStation = new Station{Latitude = t.Result.Latitude, Longitude = t.Result.Longitude};
				holder.Station = currentStation.GetClosestStation(stations);
			}, scheduler);						
		}
	}
}

