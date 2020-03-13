using Airline.DAL.Repository;
using Airline.Models.Models;
using Airline.DAL.EF;
using System.Data.Entity;
using System;

namespace Airline.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private AirlineDbContext db;
        private Repository<Flight> flightRepository;
        private Repository<CrewMember> crewmemberRepository;
        private Repository<Request> requestRepository;
        private Repository<Profile> profileRepository;
        public UnitOfWork()
        {
            db = new AirlineDbContext();
        }
        public IRepository<Flight> Flights
        {
            get
            {
                if (flightRepository == null)
                    flightRepository = new Repository<Flight>(db);
                return flightRepository;
            }
        }
        public IRepository<CrewMember> CrewMembers
        {
            get
            {
                if (crewmemberRepository == null)
                    crewmemberRepository = new Repository<CrewMember>(db);
                return crewmemberRepository;
            }
        }
        public IRepository<Request> Requests
        {
            get
            {
                if (requestRepository == null)
                    requestRepository = new Repository<Request>(db);
                return requestRepository;
            }
        }
        public IRepository<Profile> Profiles
        {
            get
            {
                if (profileRepository == null)
                    profileRepository = new Repository<Profile>(db);
                return profileRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
