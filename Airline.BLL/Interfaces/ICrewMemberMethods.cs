using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airline.DAL.UnitOfWork;
using Airline.Models.Models;

namespace Airline.BLL.Interfaces
{
    public interface ICrewMemberMethods
    {
        void Create(CrewMember crewMember);
        IEnumerable<CrewMember> GetAll();
        IEnumerable<CrewMember> GetAllNoDeleted();
        void SoftDelete(int id);
        void Restore(int id);
        void Delete(int id);
        int GetCount();
        CrewMember Get(int id);
        void Edit(CrewMember crewMember);
    }

}
