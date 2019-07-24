using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentACar.Models;
using RentACar.ViewModels;
using System.Data.Entity;


namespace RentACar.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext _context;

        public CarsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var cars = _context.Cars.Include(c => c.CarType).ToList().OrderBy(c=>c.Name);
            return View(cars);
        }

        public ActionResult New()
        {
            var carTypes = _context.CarTypes.ToList();
            var viewModel = new CarFormViewModel
            {
                Car = new Car(),
                CarTypes = carTypes
            };
            return View("CarForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Car car)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CarFormViewModel
                {
                    Car = car,
                    CarTypes = _context.CarTypes.ToList()
                };
                return View("CarForm", viewModel);
            }
            if (car.Id == 0)
            {
                _context.Cars.Add(car);
            }
            else
            {
                var carInDb = _context.Cars.Single(c => c.Id == car.Id);

                carInDb.Name = car.Name;
                carInDb.Description = car.Description;
                carInDb.CarTypeId = car.CarTypeId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Cars");
        }

        public ActionResult Edit(int id)
        {
            var car = _context.Cars.SingleOrDefault(p => p.Id == id);

            if (car == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CarFormViewModel
            {
                Car = car,
                CarTypes = _context.CarTypes.ToList()
            };
            return View("CarForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var car = _context.Cars.Find(id);

            if (car == null)
            {
                return HttpNotFound();
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cars");
        }
    }
}