using Airline.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.BLL.Interfaces
{
    public interface IFlightMethods
    {
        void Create(Flight flight);
        IEnumerable<Flight> GetAll();
        IEnumerable<Flight> GetAllNoDeleted();
        void SoftDelete(int id);
        void Restore(int id);
        void Delete(int id);
        int GetCount();
        Flight Get(int id);
        void Edit(Flight flight);
        void CrewAssigment(Flight flight);
        void Ready(int id);
        void Unready(int id);
    }
}
