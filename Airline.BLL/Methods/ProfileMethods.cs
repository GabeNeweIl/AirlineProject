using Airline.BLL.Interfaces;
using Airline.DAL.UnitOfWork;
using Airline.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.BLL.Methods
{
    public class ProfileMethods : IProfileMethods
    {
        IUnitOfWork db;
        public ProfileMethods(IUnitOfWork _db)
        {
            db = _db;
        }
        public void Create(Profile profile)
        {
            if (profile != null)
            {
                db.Profiles.Create(profile);
                db.Save();
            }
        }

        public void Edit(Profile profile)
        {

            if (profile != null && profile.Id > 0)
            {
                db.Profiles.Update(profile);
                db.Save();
            }
        }

        public Profile Get(int id)
        {
            return db.Profiles.Get(id);
        }
        public int GetByEmail(string email)
        {
            int id = db.Profiles.GetAll().First(x => x.Email == email).Id;
            return id;
        }
    }
}
