using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_MVC.Models
{
    public class AddWorkType
    {
        [Required(ErrorMessage = "Description is required")]
        [MinLength(0)]
        public string Description;
        [Required(ErrorMessage = "PaymentPerDay is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double PaymentPerDay;
    }
}
