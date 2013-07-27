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

    }
}
