using System;
using System.Collections.Generic;
using System.Linq;

namespace ChART.Domain.Entities
{
    public class Station
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public double[] Location { get; set; }
        public double Latitude { get {return Location[1];} }
        public double Longitude { get{return Location[0];} }
        public string IconUrl { get; set; }

        public Station()
        {
            Location = new double[2];
        }
    }
}
