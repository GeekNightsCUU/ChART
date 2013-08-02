using ChART.DataAccess.Abstract;
using ChARTServices.Models;
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
        public int PageSize = 20;

        public StationsController(IStationRepository stationsRepository)
        {
            this.stationsRepository = stationsRepository;
        }
        //
        // GET: /Stations/

        public ViewResult Index(int page = 1)
        {
            StationListViewModel model = new StationListViewModel { 
                Stations = stationsRepository.Stations
                .OrderBy(s => s.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo{
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = stationsRepository.Stations.Count()
                }
            };
            return View(model);
        }

        //Get: /Stations/Details/:id

        public ViewResult Details(string id)
        {
            var station = stationsRepository.Station(id);
            return View(station);
        }

        //Get: /Stations/Edit/:id

        public ViewResult Edit(string id)
        {
            var station = stationsRepository.Station(id);
            return View(station);
        }

        // GET: /routes/Troncal/stations
        public ViewResult ByRoute(string route, int page = 1)
        {
            StationListViewModel model = new StationListViewModel
            {
                Stations = stationsRepository.Stations
                .OrderBy(s => s.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Where(s => s.Route == route),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = stationsRepository.Stations.Count()
                }
            };
            return View("Index", model);
        }
    }
}
