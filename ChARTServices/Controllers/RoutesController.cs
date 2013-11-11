﻿using ChART.DataAccess.Abstract;
using ChART.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChARTServices.Controllers
{
    public class RoutesController : ApiController
    {
        private IStationRepository stationsRepository;

        public RoutesController(IStationRepository stationsRepository)
        {
            this.stationsRepository = stationsRepository;
        }
        // GET api/routes
        public IEnumerable<Station> Get()
        {
            var stations = stationsRepository.Stations.Where(s => s.Route == "Troncal").OrderBy(s => s.Name).ToList<Station>();
            stations.ForEach(s => s.Icon = null);            
            return stations;
        }

        // GET api/routes/5
        public Station Get(string id)
        {
            return stationsRepository.Station(id);
        }

        //// POST api/routes
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/routes/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/routes/5
        //public void Delete(int id)
        //{
        //}
    }
}
