using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Airline.DAL.Entities;
namespace Airline.DAL.EF
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext() : base("DefaultConnection") { }
        DbSet<CrewMember> CrewMembers { get; set; }
        DbSet<Flight> Flights { get; set; }
        DbSet<Request> Requests { get; set; }
        DbSet<Profile> Profiles { get; set; }
    }
}
