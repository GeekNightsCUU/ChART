using System;
using System.Collections.Generic;
using System.Linq;

namespace ChArt.Domain.Entities
{
    class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string IconUrl { get; set; }
    }
}
