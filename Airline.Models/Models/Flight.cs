using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Models.Models
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
        public double Price { get; set; } //цена за посадочное место
        public bool IsDeleted { get; set; } //true - активно || false - удалено
        public bool StatusReady { get; set; } //false - экипаж не назначен || true - готов
        public virtual ICollection<Profile> Profiles { get; set; } //связь пассажиров к рейсу
        public virtual ICollection<CrewMember> CrewMembers { get; set; }
        public Flight()
        {
            Profiles = new List<Profile>();
            CrewMembers = new List<CrewMember>();
        }
    }
}
