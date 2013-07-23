using ChART.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChARTServices.Controllers
{
    public class StationsController : Controller
    {
        private IStationRepository stationsRepository;

        public StationsController(IStationRepository stationsRepository)
        {
            this.stationsRepository = stationsRepository;
        }
        //
        // GET: /Stations/

        public ViewResult Index()
        {
            return View(stationsRepository.Stations);
        }

    }
}
