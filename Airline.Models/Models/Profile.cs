using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Models.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PassportID { get; set; } //номер пасспорта
        public DateTime Birth { get; set; } //дата рождения
        public virtual ICollection<Flight> Flights { get; set; }
        public Profile()
        {
            Flights = new List<Flight>();
        }
    }
}
