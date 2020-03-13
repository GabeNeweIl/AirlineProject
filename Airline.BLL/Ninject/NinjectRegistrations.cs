using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airline.DAL.Repository;
using Airline.BLL.Interfaces;
using Airline.BLL.Methods;
using Airline.DAL.UnitOfWork;
using Ninject.Web.Common;

namespace Airline.BLL.Ninject
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            Bind<ICrewMemberMethods>().To<CrewMemberMethods>();
            Bind<IRequestMethods>().To<RequestMethods>();
            Bind<IFlightMethods>().To<FlightMethods>();
            Bind<IProfileMethods>().To<ProfileMethods>();
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
        }
    }
}
