using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hotel_site.Models;
using hotel_site.Repository;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRepository<HotelInfo> _hotelInfoDb;
        private IRepository<HotelBuilding> _hotelBuildingDb;
        //private DbRepository _dbRepository;

        public HomeController(ILogger<HomeController> logger, HotelInfoDbRepository hotelInfoDb, HotelBuildingDbRepository hotelBuildingDb)
        {
            _logger = logger;
            //_dbRepository = dbRepository;
            _hotelInfoDb = hotelInfoDb;
            _hotelBuildingDb = hotelBuildingDb;
        }

        public IActionResult Index()
        {
            return View(_hotelInfoDb.GetEntity(1));
        }

        public IActionResult SetHotelInfo()
        {
            return View(_hotelInfoDb.GetEntity(1));
        }

        [Authorize]
        [HttpPost]
        public IActionResult SetHotelInfo(string name, string description, string phoneNumber, string email)
        {
            try
            {
                HotelInfo hotelInfo = new HotelInfo(1, name, description, phoneNumber, email);
                _hotelInfoDb.Delete(1);
                _hotelInfoDb.Create(hotelInfo);
                return Index();
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        public IActionResult Comments()
        {
            return View();
        }

        public IActionResult Photo()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
