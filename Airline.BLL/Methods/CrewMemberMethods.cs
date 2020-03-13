using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airline.DAL.UnitOfWork;
using Airline.BLL.Interfaces;
using Airline.Models.Models;

namespace Airline.BLL.Methods
{
    public class CrewMemberMethods : ICrewMemberMethods
    {
        IUnitOfWork db;
        public CrewMemberMethods(IUnitOfWork _db)
        {
            db = _db;
        }
        public void Create(CrewMember crewMember)
        {
            if (crewMember != null)
            {
                CrewMember crew = new CrewMember
                {
                    Name = crewMember.Name,
                    Surname = crewMember.Surname,
                    Age = crewMember.Age,
                    Position = crewMember.Position,
                    IsDeleted = true
                };
                db.CrewMembers.Create(crew);
                db.Save();
            }
        }
        public IEnumerable<CrewMember> GetAll()
        {
            return db.CrewMembers.GetAll();
        }
        public IEnumerable<CrewMember> GetAllNoDeleted()
        {
            IEnumerable<CrewMember> crewMembers = db.CrewMembers.GetAll().Where(x => x.IsDeleted == true);
            return crewMembers;
        }
        public void SoftDelete(int id) // изменениея статуса на удален
        {
            int? check = id;
            if (check != null && check > 0)
            {
                db.CrewMembers.Get(id).IsDeleted = false;
                db.CrewMembers.Update(db.CrewMembers.Get(id));
                db.Save();
            }
        }
        public void Restore(int id) //восстановления статуса из удален
        {
            int? check = id;
            if (check != null && check > 0)
            {
                db.CrewMembers.Get(id).IsDeleted = true;
                db.CrewMembers.Update(db.CrewMembers.Get(id));
                db.Save();
            }
        }
        public CrewMember Get(int id)
        { 
            return db.CrewMembers.Get(id);
        }
        public void Edit(CrewMember crewMember)
        {
            if (crewMember != null)
            {
                db.CrewMembers.Update(crewMember);
                db.Save();
            }
        }
        public int GetCount()
        {
            return db.CrewMembers.GetCount();
        }
        public void Delete(int id) //полное удаление
        {
            int? check = id;
            if (check != null && check > 0)
            {
                db.CrewMembers.Delete(db.CrewMembers.Get(id));
                db.Save();
            }
        }
    }

}
