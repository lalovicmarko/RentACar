using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar.ViewModels
{
    public class RentalFormViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public Rental Rental { get; set; }
    }
}