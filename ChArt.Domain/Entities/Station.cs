using Newtonsoft.Json;
using System;
using System.Drawing;


namespace ChART.Domain.Entities
{
    public class Station
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("location")]
        public double[] Location { get; set; }
        [JsonProperty("latitute")]
        public double Latitude { get {return Location[1];} set{ Location[1] = value; } }
        [JsonProperty("longitude")]
        public double Longitude { get{return Location[0];} set{ Location[0] = value; } }
        [JsonProperty("route")]
        public string Route { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }

        public Station()
        {
            Location = new double[2];
        }

		public static readonly PointF TroncalRouteCenter = new PointF (-106.07476f, 28.639196f);

		public string ImageFilename()
		{
			return Name.Replace("Estación","").TrimStart().Replace (" ", "-") + ".png";
		}
    }
}
