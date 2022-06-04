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
    public class ServiceOrderController : Controller
    {
        private readonly ILogger<ServiceOrderController> _logger;
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

        public ServiceOrderController(ILogger<ServiceOrderController> logger,
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
            return View(_serviceOrderDb.GetEntityList());
        }

        public IActionResult AddServiceOrder(int serviceId)
        {
            return View(_serviceDb.GetEntity(serviceId));
        }

        [HttpPost]
        public async Task<IActionResult> AddServiceOrder(int serviceId, bool confirm)
        {
            try
            {
                Service service = _serviceDb.GetEntity(serviceId);
                ServiceOrder serviceOrder = new ServiceOrder(_serviceOrderDb.GetNewId(), service.Price);
                User user = await _userManager.GetUserAsync(User);
                Room room = null;
                foreach (Book book in _bookDb.GetEntityList())
                    if (book.IsActive() && book.UserId == user.Id)
                        room = _roomDb.GetEntity(book.RoomId);
                if (room == null)
                    return View("ErrorPage", "Вы должны быть постояльцем отеля, чтобы заказать услугу.");
                serviceOrder.Link(service, user, room);
                _serviceOrderDb.Create(serviceOrder);
                return RedirectToAction("Index", "Service");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteServiceOrder(int id)
        {
            return View(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DeleteServiceOrder(int id, bool confirm)
        {
            try
            {
                _serviceOrderDb.Delete(id);
                return RedirectToAction("Index");
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
