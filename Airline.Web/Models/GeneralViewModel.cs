using Airline.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Airline.Web.Models
{
    public class Search
    {
        [Required]
        [Display(Name = "City of departure")]
        public string From { get; set; }
        [Required]
        [Display(Name = "City of arrival")]
        public string To { get; set; }
        [Required]
        [Display(Name = "Date of arrival")]
        public DateTime? DateDeparture { get; set; }
    }
    public class SearchByNumber
    {
        [Required]
        [Display(Name = "Flight number")]
        public int Number { get; set; }
    }
    public class PageViewMdodel
    {
        public IEnumerable<Flight> Flights { get; set; }
        public Page Page { get; set; }
    }
}