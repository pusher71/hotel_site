using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using hotel_site.Models;
using hotel_site.Repository;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IRepository<Service> _serviceDb;

        public ServiceController(ServiceDbRepository serviceDb)
        {
            _serviceDb = serviceDb;
        }

        public IActionResult Index()
        {
            return View(_serviceDb.GetEntityList());
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddService()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddService(string name, float price)
        {
            if (name == null)
                return View("ErrorPage", "Название услуги не должно быть пустым.");

            try
            {
                Service service = new Service(_serviceDb.GetNewId(), name, price);
                _serviceDb.Create(service);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditService(int id)
        {
            return View(_serviceDb.GetEntity(id));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditService(int id, string name, float price)
        {
            if (name == null)
                return View("ErrorPage", "Название услуги не должно быть пустым.");

            try
            {
                Service service = _serviceDb.GetEntity(id);
                service.Name = name;
                service.Price = price;
                _serviceDb.Update(service);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteService(int id)
        {
            return View(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DeleteService(int id, bool confirm)
        {
            try
            {
                _serviceDb.Delete(id);
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
