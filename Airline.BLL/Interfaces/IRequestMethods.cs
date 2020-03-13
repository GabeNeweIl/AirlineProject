using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airline.Models.Models;

namespace Airline.BLL.Interfaces
{
    public interface IRequestMethods
    {
        void Create(Request request);
        void StatusDone(int id);
        void StatusReject(int id);
        int GetCount();
        IEnumerable<Request> GetAll();
    }
}
