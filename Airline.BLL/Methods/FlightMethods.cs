using Airline.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airline.Models.Models;
using Airline.DAL.UnitOfWork;

namespace Airline.BLL.Methods
{
    public class FlightMethods : IFlightMethods
    {
        IUnitOfWork db;
        public FlightMethods(IUnitOfWork _db)
        {
            db = _db;
        }
        public void Create(Flight flight)
        {
            if (flight != null)
            {
                Flight newFlight = new Flight
                {
                    Departure = flight.Departure,
                    Arrival = flight.Arrival,
                    FromCity = flight.FromCity,
                    FromCountry = flight.FromCountry,
                    ToCity = flight.ToCity,
                    ToCountry = flight.ToCountry,
                    TotalNumberPassengers = flight.TotalNumberPassengers,
                    Price = flight.Price,
                    CurrentNumberPassengers = 0,
                    IsDeleted = true
                };
                db.Flights.Create(newFlight);
                db.Save();
            }
        }
        public IEnumerable<Flight> GetAll()
        {
            return db.Flights.GetAll();
        }
        public IEnumerable<Flight> GetAllNoDeleted()
        {
            IEnumerable<Flight> flights = db.Flights.GetAll().Where(x => x.IsDeleted == true);
            return flights;
        }
        public void SoftDelete(int id) //изменения статуса на удален
        {
            int? check = id;
            if (check != null && check > 0)
            {
                db.Flights.Get(id).IsDeleted = false;
                db.Flights.Update(db.Flights.Get(id));
                db.Save();
            }
        }
        public void Restore(int id) //восстановление из статуса удален
        {
            int? check = id;
            if (check != null && check > 0)
            {
                db.Flights.Get(id).IsDeleted = true;
                db.Flights.Update(db.Flights.Get(id));
                db.Save();
            }
        }
        public Flight Get(int id)
        {
            return db.Flights.Get(id);
        }
        public void Edit(Flight flight)
        {
            if (flight != null)
            {
                db.Flights.Update(flight);
                db.Save();
            }
        }
        public int GetCount()
        {
            return db.Flights.GetCount();
        }
        public void CrewAssigment(Flight flight) //добавление экипажа к рейсу и назначение статуса ready
        {
            if (flight != null)
            {
                flight.StatusReady = true;
                db.Flights.Update(flight);
                db.Save();
            }
        }
        public void Ready(int id) //измененеия статуса на ready
        {
            int? check = id;
            if (check != null && id > 0)
            {
                db.Flights.Get(id).StatusReady = true;
                db.Flights.Update(db.Flights.Get(id));
                db.Save();
            }
        }
        public void Unready(int id) //измененеия статуса на unready
        {
            int? check = id;
            if (check != null && id > 0)
            {
                db.Flights.Get(id).StatusReady = false;
                db.Flights.Update(db.Flights.Get(id));
                db.Save();
            }
        }
        public void Delete(int id) //полное удаление
        {
            int? check = id;
            if (check != null && check > 0)
            {
                db.Flights.Delete(db.Flights.Get(id));
                db.Save();
            }
        }
    }
}
