using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        public Car Car { get; set; }
        [Display(Name = "Car")]
        public int CarId { get; set; }

        public Customer Customer { get; set; }
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
    }
}