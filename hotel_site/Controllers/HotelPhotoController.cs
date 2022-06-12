using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using hotel_site.Models;
using hotel_site.Repository;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class HotelPhotoController : Controller
    {
        private readonly IRepository<HotelBuilding> _hotelBuildingDb;
        private readonly IRepository<HotelPhoto> _hotelPhotoDb;

        public HotelPhotoController(HotelBuildingDbRepository hotelBuildingDb, HotelPhotoDbRepository hotelPhotoDb)
        {
            _hotelBuildingDb = hotelBuildingDb;
            _hotelPhotoDb = hotelPhotoDb;
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddHotelPhoto(int hotelBuildingId)
        {
            return View(hotelBuildingId);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddHotelPhoto(int hotelBuildingId, IFormFile uploadImage)
        {
            if (uploadImage == null)
                return View("ErrorPage", "Изображение не загружено.");

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

        [Authorize(Roles = "admin")]
        public IActionResult DeleteHotelPhoto(int id)
        {
            return View(_hotelPhotoDb.GetEntity(id));
        }

        [Authorize(Roles = "admin")]
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
