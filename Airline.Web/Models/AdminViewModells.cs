using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Airline.Web.Models
{
    public class NewCrewMemberViewModel
    {
        [Required]
        [Display(Name ="Position")]
        public string Position { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Age (must be between 18 and 55)")]
        public int Age { get; set; }
    }
    public class EditCrewMemberViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Position")]
        public string Position { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Age (must be between 18 and 55)")]
        public int Age { get; set; }
    }
    public class NewFlightViewModel
    {
        [Required]
        [Display(Name = "Departure")]
        public DateTime Departure { get; set; }
        [Required]
        [Display(Name = "Arraival")]
        public DateTime Arraival { get; set; }
        [Required]
        [Display(Name = "From Country")]
        public string FromCountry { get; set; }
        [Required]
        [Display(Name = "From City")]
        public string FromCity { get; set; }
        [Required]
        [Display(Name = "To Country")]
        public string ToCountry { get; set; }
        [Required]
        [Display(Name = "To City")]
        public string ToCity { get; set; }
        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }
        [Required]
        [Range(1,100)]
        [Display(Name = "Total number of passangers")]
        public int Total { get; set; }

    }
    public class EditFlightViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Departure")]
        public DateTime Departure { get; set; }
        [Required]
        [Display(Name = "Arraival")]
        public DateTime Arraival { get; set; }
        [Required]
        [Display(Name = "From Country")]
        public string FromCountry { get; set; }
        [Required]
        [Display(Name = "From City")]
        public string FromCity { get; set; }
        [Required]
        [Display(Name = "To Country")]
        public string ToCountry { get; set; }
        [Required]
        [Display(Name = "To City")]
        public string ToCity { get; set; }
        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Total numbers of passangers")]
        public int Total { get; set; }

    }
}