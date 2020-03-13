using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Airline.Models.Models;

namespace Airline.DAL.EF
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext() : base("DefaultConnection") { }
        public DbSet<CrewMember> CrewMembers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Profile> Profiles { get; set; }
    }
}
