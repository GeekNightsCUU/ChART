using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChART.Domain.Entities
{
    public class Station
    {
        [JsonIgnore]
        public Object Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("location")]
        public double[] Location { get; set; }
        public double Latitude { get {return Location[1];} }
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
