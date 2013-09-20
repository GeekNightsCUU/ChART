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
		public static void CurrentClosestStation(IQueryable<Station> stations, IStationHolder holder, TaskScheduler scheduler, Geolocator geolocator)
		{		
			geolocator.DesiredAccuracy = 50;
			var locator = geolocator;
			locator.GetPositionAsync (10000).ContinueWith (t => {
				if(t.Status == TaskStatus.RanToCompletion){
					var currentStation = new Station{Latitude = t.Result.Latitude, Longitude = t.Result.Longitude};
					holder.Station = currentStation.GetClosestStation(stations);
				}else{
					var currentStation = Station.NotFoundStation();
					holder.Station = currentStation;
				}

			}, scheduler);					
		}
	}
}

