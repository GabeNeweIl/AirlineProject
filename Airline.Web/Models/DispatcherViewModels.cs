using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Airline.Models.Models;
namespace Airline.Web.Models
{
    public class SendRequestViewModel
    {
        [Required]
        [Display(Name = "Text")]
        public string Text { get; set; }
    }
    public class AssignCrewViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Pilot")]
        public int? PilotId { get; set; }
        [Required]
        [Display(Name = "RadioOperator")]
        public int? RadioOperatorId { get; set; }
        [Required]
        [Display(Name = "Navigator")]
        public int? NavigatorId { get; set; }
        [Required]
        [Display(Name = "Stewardess")]
        public int? StewardessId { get; set; }
    }
}