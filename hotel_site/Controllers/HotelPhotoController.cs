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
    public class HotelPhotoController : Controller
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

        public HotelPhotoController(ILogger<HomeController> logger,
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

        public IActionResult AddHotelPhoto(int hotelBuildingId)
        {
            return View(hotelBuildingId);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddHotelPhoto(int hotelBuildingId, IFormFile uploadImage)
        {
            try
            {
                //перевести переданный файл в массив байтов
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadImage.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadImage.Length);
                }

                HotelPhoto hotelPhoto = new HotelPhoto(_hotelPhotoDb.GetNewId(), imageData); //созданная фотография отеля
                HotelBuilding hotelBuilding = _hotelBuildingDb.GetEntity(hotelBuildingId); //связанный корпус отеля

                hotelPhoto.SetHotel(hotelBuilding);
                _hotelPhotoDb.Create(hotelPhoto);
                return RedirectToAction("Index", "HotelBuilding", new { id = hotelPhoto.HotelBuildingId });
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        public IActionResult DeleteHotelPhoto(int id)
        {
            return View(id);
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteHotelPhoto(int id, bool confirm)
        {
            try
            {
                HotelPhoto hotelPhoto = _hotelPhotoDb.GetEntity(id);
                _hotelPhotoDb.Delete(id);
                return RedirectToAction("Index", "HotelBuilding", new { id = hotelPhoto.HotelBuildingId });
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