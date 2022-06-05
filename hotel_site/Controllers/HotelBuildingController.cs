using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using hotel_site.Models;
using hotel_site.Models.ViewModels;
using hotel_site.Repository;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class HotelBuildingController : Controller
    {
        private readonly ILogger<HotelBuildingController> _logger;
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
        private readonly IRepository<ServiceOrder> _serviceOrderDb;

        public HotelBuildingController(ILogger<HotelBuildingController> logger,
            UserManager<User> userManager,
            HotelInfoDbRepository hotelInfoDb,
            HotelBuildingDbRepository hotelBuildingDb,
            HotelPhotoDbRepository hotelPhotoDb,
            RoomDbRepository roomDb,
            RoomPhotoDbRepository roomPhotoDb,
            BookDbRepository bookDb,
            CommentDbRepository commentDb,
            MessageDbRepository messageDb,
            ServiceDbRepository serviceDb,
            ServiceOrderDbRepository serviceOrderDb)
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
            _serviceOrderDb = serviceOrderDb;
        }

        public IActionResult Index(int id)
        {
            return View(_hotelBuildingDb.GetEntity(id));
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddHotelBuilding()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddHotelBuilding(string name, string description, string address, string phoneNumber, string email)
        {
            if (name == null)
                return View("ErrorPage", "Название не должно быть пустым.");
            if (description == null)
                return View("ErrorPage", "Описание не должно быть пустым.");
            if (address == null)
                return View("ErrorPage", "Адрес не должен быть пустым.");
            if (phoneNumber == null)
                return View("ErrorPage", "Контактный телефон не должен быть пустым.");
            if (email == null)
                return View("ErrorPage", "E-mail не должен быть пустым.");

            try
            {
                HotelBuilding hotelBuilding = new HotelBuilding(_hotelBuildingDb.GetNewId(), name, description, address, phoneNumber, email);
                _hotelBuildingDb.Create(hotelBuilding);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditHotelBuilding(int id)
        {
            return View(_hotelBuildingDb.GetEntity(id));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditHotelBuilding(int id, string name, string description, string address, string phoneNumber, string email)
        {
            if (name == null)
                return View("ErrorPage", "Название не должно быть пустым.");
            if (description == null)
                return View("ErrorPage", "Описание не должно быть пустым.");
            if (address == null)
                return View("ErrorPage", "Адрес не должен быть пустым.");
            if (phoneNumber == null)
                return View("ErrorPage", "Контактный телефон не должен быть пустым.");
            if (email == null)
                return View("ErrorPage", "E-mail не должен быть пустым.");

            try
            {
                HotelBuilding hotelBuilding = _hotelBuildingDb.GetEntity(id);
                hotelBuilding.Name = name;
                hotelBuilding.Description = description;
                hotelBuilding.Address = address;
                hotelBuilding.PhoneNumber = phoneNumber;
                hotelBuilding.Email = email;
                _hotelBuildingDb.Update(hotelBuilding);
                return RedirectToAction("Index", new { id = hotelBuilding.Id });
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteHotelBuilding(int id)
        {
            return View(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DeleteHotelBuilding(int id, bool confirm)
        {
            try
            {
                _hotelBuildingDb.Delete(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
