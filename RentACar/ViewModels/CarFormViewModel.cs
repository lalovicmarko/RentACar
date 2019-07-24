using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar.ViewModels
{
    public class CarFormViewModel
    {
        public IEnumerable<CarType> CarTypes { get; set; }
        public Car Car{ get; set; }
    }
}