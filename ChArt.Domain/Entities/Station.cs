using Newtonsoft.Json;
using System;
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
        public double Latitude { get {return Location[1];} }
        [JsonProperty("longitude")]
        public double Longitude { get{return Location[0];} }
        [JsonProperty("route")]
        public string Route { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }

        public Station()
        {
            Location = new double[2];
        }
    }
}
