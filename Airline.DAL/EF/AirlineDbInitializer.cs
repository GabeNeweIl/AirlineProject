using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Airline.DAL.EF
{
    public class AirlineDbInitializer : CreateDatabaseIfNotExists<AirlineDbContext>
    {
        protected override void Seed(AirlineDbContext context)
        {
            base.Seed(context);
        }
    }
}
