using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Airline.Web.Models
{
    public class ProfileViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "PassportID")]
        public string PassportID { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime Birth { get; set; }

    }
}