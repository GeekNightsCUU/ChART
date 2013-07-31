using ChART.DataAccess.Abstract;
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
            var stations = stationsRepository.Stations.OrderBy(s => s.Name);
            return stations;
        }

        // GET api/routes/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

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
