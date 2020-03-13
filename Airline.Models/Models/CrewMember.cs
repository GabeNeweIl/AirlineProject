using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Models.Models
{
    public class CrewMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; } //должность
        public int Age { get; set; }
        public bool IsDeleted { get; set; } //true - активно || false - удалены
        public virtual ICollection<Flight> Flights { get; set; }
        public CrewMember()
        {
            Flights = new List<Flight>();
        }

    }
}
