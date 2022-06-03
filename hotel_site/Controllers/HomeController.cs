using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using hotel_site.Models;
using hotel_site.Models.ViewModels;
using hotel_site.Repository;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<HotelInfo> _hotelInfoDb;
        private readonly IRepository<HotelBuilding> _hotelBuildingDb;
        private readonly IRepository<HotelPhoto> _hotelPhotoDb;
        private readonly IRepository<Room> _roomDb;
        private readonly IRepository<RoomPhoto> _roomPhotoDb;
        private readonly IRepository<Book> _bookDb;
        private readonly IRepository<Comment> _commentDb;
        private readonly IRepository<Message> _messageDb;
        private readonly IRepository<Service> _serviceDb;

        public HomeController(ILogger<HomeController> logger,
            UserManager<User> userManager,
            HotelInfoDbRepository hotelInfoDb,
            HotelBuildingDbRepository hotelBuildingDb,
            HotelPhotoDbRepository hotelPhotoDb,
            RoomDbRepository roomDb,
            RoomPhotoDbRepository roomPhotoDb,
            BookDbRepository bookDb,
            CommentDbRepository commentDb,
            MessageDbRepository messageDb,
            ServiceDbRepository serviceDb)
        {
            _logger = logger;
            _userManager = userManager;
            _hotelInfoDb = hotelInfoDb;
            _hotelBuildingDb = hotelBuildingDb;
            _hotelPhotoDb = hotelPhotoDb;
            _roomDb = roomDb;
            _roomPhotoDb = roomPhotoDb;
            _bookDb = bookDb;
            _commentDb = commentDb;
            _messageDb = messageDb;
            _serviceDb = serviceDb;
        }

        private HotelInfoAndBuildings GetHotelInfoAndBuildings()
        {
            return new HotelInfoAndBuildings
            {
                HotelInfo = _hotelInfoDb.GetEntity(1),
                HotelBuildings = _hotelBuildingDb.GetEntityList()
            };
        }

        public IActionResult Index()
        {
            return View(GetHotelInfoAndBuildings());
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditHotelInfo()
        {
            return View(_hotelInfoDb.GetEntity(1));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditHotelInfo(string name, string description, string phoneNumber, string email)
        {
            try
            {
                HotelInfo hotelInfo = _hotelInfoDb.GetEntity(1);
                hotelInfo.Name = name;
                hotelInfo.Description = description;
                hotelInfo.PhoneNumber = phoneNumber;
                hotelInfo.Email = email;
                _hotelInfoDb.Update(hotelInfo);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        public IActionResult Photo()
        {
            return View(_hotelBuildingDb.GetEntity(1));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
