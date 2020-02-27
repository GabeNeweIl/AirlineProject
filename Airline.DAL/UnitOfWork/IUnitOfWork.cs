using Airline.DAL.Repository;
using Airline.DAL.Entities;

namespace Airline.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Flight> Flights { get; }
        IRepository<CrewMember> CrewMembers { get; }
        IRepository<Profile> Profiles { get; }
        IRepository<Request> Requests { get; }
        void Save();
    }
}
