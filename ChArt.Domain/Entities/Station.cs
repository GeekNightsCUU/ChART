using System;
using System.Collections.Generic;
using System.Linq;

namespace ChART.Domain.Entities
{
    public class Station
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string IconUrl { get; set; }
    }
}
