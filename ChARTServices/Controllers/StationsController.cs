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
        public int PageSize = 3;

        public StationsController(IStationRepository stationsRepository)
        {
            this.stationsRepository = stationsRepository;
        }
        //
        // GET: /Stations/

        public ViewResult Index(int page = 1)
        {
            return View(stationsRepository.Stations
                .OrderBy(s => s.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize));
        }

    }
}
