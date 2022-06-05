using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using hotel_site.Models;
using hotel_site.Repository;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class ServiceOrderController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Room> _roomDb;
        private readonly IRepository<Book> _bookDb;
        private readonly IRepository<Service> _serviceDb;
        private readonly IRepository<ServiceOrder> _serviceOrderDb;

        public ServiceOrderController(UserManager<User> userManager,
            RoomDbRepository roomDb,
            BookDbRepository bookDb,
            ServiceDbRepository serviceDb,
            ServiceOrderDbRepository serviceOrderDb)
        {
            _userManager = userManager;
            _roomDb = roomDb;
            _bookDb = bookDb;
            _serviceDb = serviceDb;
            _serviceOrderDb = serviceOrderDb;
        }

        public IActionResult Index()
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
