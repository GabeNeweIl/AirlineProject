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
    public class RequestMethods : IRequestMethods
    {
        IUnitOfWork db;
        public RequestMethods(IUnitOfWork _db)
        {
            db = _db;
        }
        public void StatusDone(int id) //управление статусами на выполненно
        {
            db.Requests.Get(id).StatusBefore = true;
            db.Requests.Get(id).StatusAfter = true;
            db.Requests.Update(db.Requests.Get(id));
            db.Save();
        }

        public void Create(Request request)
        {
            if (request != null)
            {
                Request newRequest = new Request
                {
                    Text = request.Text,
                    StatusBefore = false,
                    StatusAfter = false,
                };
                db.Requests.Create(newRequest);
                db.Save();
            }
        }

        public IEnumerable<Request> GetAll()
        {
            return db.Requests.GetAll();
        }

        public int GetCount()
        {
            return db.Requests.GetCount();
        }

        public void StatusReject(int id) //управление статусом на отклоненно
        {
            db.Requests.Get(id).StatusBefore = true;
            db.Requests.Get(id).StatusAfter = false;
            db.Requests.Update(db.Requests.Get(id));
            db.Save();
        }
    }
}
