using Airline.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.BLL.Interfaces
{
    public interface IProfileMethods
    {
        void Create(Profile profile);
        Profile Get(int id);
        void Edit(Profile profile);
        int GetByEmail(string email);
    }
}
