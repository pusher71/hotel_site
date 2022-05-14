using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hotel_site.Models;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DbRepository _dbRepository;

        public HomeController(ILogger<HomeController> logger, DbRepository dbRepository)
        {
            _logger = logger;
            _dbRepository = dbRepository;
        }

        public IActionResult Index()
        {
            return View(_dbRepository.GetHotel());
        }

        public IActionResult SetHotelInfo()
        {
            return View(_dbRepository.GetHotel());
        }

        [Authorize]
        [HttpPost]
        public IActionResult SetHotelInfo(string name, string description, string address, string phoneNumber, string email)
        {
            try
            {
                Hotel hotel = new Hotel(1, name, description, address, phoneNumber, email);
                _dbRepository.SetHotel(hotel);
                return View("Index", _dbRepository.GetHotel());
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
