using System;
using System.Collections.Generic;
using System.Linq;
using ChART.Domain.Entities;

namespace ChART.Domain.Entities
{
	public static class StationExtensions
	{
		public static Station GetClosestStation(this Station station, IQueryable<Station> stations)
		{
			if (stations.Count () == 0) {
				return Station.NotFoundStation ();
			}
			return stations.OrderBy (currentStation => SortableApproximateDistance (station, currentStation)).First ();
		}

		private static double SortableApproximateDistance(Station source, Station target)
		{
			return Math.Pow (target.Latitude - source.Latitude, 2) + Math.Pow (target.Longitude - source.Longitude, 2);
		}
	}
}

