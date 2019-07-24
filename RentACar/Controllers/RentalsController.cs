using RentACar.Models;
using RentACar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace RentACar.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var rental = _context.Rentals.Include(r => r.Car).Include(r => r.Customer).ToList().OrderByDescending(r => r.DateFrom);
            return View(rental);
        }


        public ActionResult New()
        {
            var viewModel = new RentalFormViewModel()
            {
                Rental = new Rental(),
                Cars = _context.Cars.ToList(),
                Customers = _context.Customers.ToList()
            };
            return View("RentalForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Rental rental)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new RentalFormViewModel()
                {
                    Rental = rental,
                    Cars = _context.Cars.ToList(),
                    Customers = _context.Customers.ToList()
                };

                return View("RentalForm", viewModel);
            }

            if (rental.Id == 0)
            {
                _context.Rentals.Add(rental);
            }
            else
            {
                var rentalInDb = _context.Rentals.Single(r => r.Id == rental.Id);

                rentalInDb.DateFrom = rental.DateFrom;
                rentalInDb.DateTo = rental.DateTo;
                rentalInDb.CarId = rental.CarId;
                rentalInDb.CustomerId = rental.CustomerId;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Rentals");
        }

        public ActionResult Edit(int id)
        {
            var rental = _context.Rentals.SingleOrDefault(r => r.Id == id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            var viewModel = new RentalFormViewModel()
            {
                Rental = rental,
                Cars = _context.Cars.ToList(),
                Customers = _context.Customers.ToList()
            };
            return View("RentalForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var rental = _context.Rentals.Find(id);

            if (rental == null)
            {
                return HttpNotFound();
            }

            _context.Rentals.Remove(rental);
            _context.SaveChanges();
            return RedirectToAction("Index", "Rentals");
        }
    }
}