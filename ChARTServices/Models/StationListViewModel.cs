using System;
using System.Collections.Generic;
using ChART.Domain.Entities;

namespace ChARTServices.Models
{
    public class StationListViewModel
    {
        public IEnumerable<Station> Stations { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}