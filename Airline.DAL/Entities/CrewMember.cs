using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.DAL.Entities
{
    public class CrewMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; } //должность
        public int Age { get; set; }
        public bool IsDeleted { get; set; } //мягкое удаление
        //Связь членов экипажа к рейсам
        public virtual ICollection<Flight> Flights { get; set; }
        public CrewMember()
        {
            Flights = new List<Flight>();
        }
    }
}
