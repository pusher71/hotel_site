﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
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
        private IRepository<HotelInfo> _hotelInfoDb;
        private IRepository<HotelBuilding> _hotelBuildingDb;
        private IRepository<HotelPhoto> _hotelPhotoDb;
        private IRepository<Room> _roomDb;
        private IRepository<RoomPhoto> _roomPhotoDb;
        private IRepository<User> _userDb;
        private IRepository<Comment> _commentDb;
        private IRepository<Message> _messageDb;
        private IRepository<Service> _serviceDb;
        private IRepository<HistoryAction> _historyActionDb;
        private IRepository<HistoryRecord> _historyRecordDb;

        public HomeController(ILogger<HomeController> logger,
            HotelInfoDbRepository hotelInfoDb,
            HotelBuildingDbRepository hotelBuildingDb,
            HotelPhotoDbRepository hotelPhotoDb,
            RoomDbRepository roomDb,
            RoomPhotoDbRepository roomPhotoDb,
            UserDbRepository userDb,
            CommentDbRepository commentDb,
            MessageDbRepository messageDb,
            ServiceDbRepository serviceDb,
            HistoryActionDbRepository historyActionDb,
            HistoryRecordDbRepository historyRecordDb)
        {
            _logger = logger;
            _hotelInfoDb = hotelInfoDb;
            _hotelBuildingDb = hotelBuildingDb;
            _hotelPhotoDb = hotelPhotoDb;
            _roomDb = roomDb;
            _roomPhotoDb = roomPhotoDb;
            _userDb = userDb;
            _commentDb = commentDb;
            _messageDb = messageDb;
            _serviceDb = serviceDb;
            _historyActionDb = historyActionDb;
            _historyRecordDb = historyRecordDb;
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

        public IActionResult EditHotelInfo()
        {
            return View(_hotelInfoDb.GetEntity(1));
        }

        [Authorize]
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

        public IActionResult Comments()
        {
            return View();
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
