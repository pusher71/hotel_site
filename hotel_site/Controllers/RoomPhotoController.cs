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
    public class RoomPhotoController : Controller
    {
        private readonly IRepository<Room> _roomDb;
        private readonly IRepository<RoomPhoto> _roomPhotoDb;

        public RoomPhotoController(RoomDbRepository roomDb, RoomPhotoDbRepository roomPhotoDb)
        {
            _roomDb = roomDb;
            _roomPhotoDb = roomPhotoDb;
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddRoomPhoto(int roomId)
        {
            return View(roomId);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddRoomPhoto(int roomId, IFormFile uploadImage)
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

                RoomPhoto roomPhoto = new RoomPhoto(_roomPhotoDb.GetNewId(), imageData); //созданная фотография номера
                Room room = _roomDb.GetEntity(roomId); //связанный номер

                roomPhoto.SetRoom(room);
                _roomPhotoDb.Create(roomPhoto);
                return RedirectToAction("Index", "Room", new { id = roomPhoto.RoomId });
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteRoomPhoto(int id)
        {
            return View(_roomPhotoDb.GetEntity(id));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DeleteRoomPhoto(int id, bool confirm)
        {
            try
            {
                RoomPhoto roomPhoto = _roomPhotoDb.GetEntity(id);
                _roomPhotoDb.Delete(id);
                return RedirectToAction("Index", "Room", new { id = roomPhoto.RoomId });
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
