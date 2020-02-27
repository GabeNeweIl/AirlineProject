using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.DAL.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime Departure { get; set; } //дата вылета
        public DateTime Arrival { get; set; } //дата прибытия
        public string FromCountry { get; set; } //откуда вылет страна
        public string ToCountry { get; set; } //куда прибудем страна
        public string FromCity { get; set; } //откуда вылет город 
        public string ToCity { get; set; } //куда прибудем город
        public int TotalNumberPassengers { get; set; } //вместимость по пассажирам
        public int CurrentNumberPassengers { get; set; } //текущее кол-во пассажиров
        public bool IsDeleted { get; set; } //мягкое удаление
        public virtual ICollection<CrewMember> CrewMembers { get; set; } //связь персонала к рейcам
        public virtual ICollection<Profile> Profiles { get; set; } //связь пассажиров к рейсу
        public Flight()
        {
            CrewMembers = new List<CrewMember>();
            Profiles = new List<Profile>();
        }
    }
}
