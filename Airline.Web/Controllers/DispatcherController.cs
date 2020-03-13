using Airline.BLL.Methods;
using Airline.Models.Models;
using Airline.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airline.BLL.Interfaces;
using NLog;

namespace Airline.Web.Controllers
{
    [Authorize(Roles = "dispatcher")]
    public class DispatcherController : Controller
    {
        ICrewMemberMethods crewMember;
        IFlightMethods flightMethods;
        IRequestMethods requestMethods;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public DispatcherController(ICrewMemberMethods _crew, IFlightMethods _flight, IRequestMethods _request)
        {
            crewMember = _crew;
            flightMethods = _flight;
            requestMethods = _request;
        }
        // GET: Dispatcher
        public ActionResult DispatcherPanel()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SendRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendRequest(SendRequestViewModel send) //отправление запроса админу 
        {
            if (ModelState.IsValid)
            {
                Request request = new Request
                {
                    Text = send.Text
                };
                requestMethods.Create(request);
                return RedirectToAction("DispatcherPanel", "Dispatcher");
            }
            else
                return View(send);
        }
        public ActionResult AllFlightsDispatcher(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().Where(x => x.IsDeleted == true).Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<Flight> flightsCount = flightMethods.GetAll().Where(x => x.IsDeleted == true);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightsCount.Count() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            return View(pageView);
        }
        [HttpGet]
        public ActionResult AssignCrew()
        {
            ViewBag.AllCrewMembers = crewMember.GetAll();
            return View();
        }
        [HttpPost]
        public ActionResult AssignCrew(AssignCrewViewModel assign) //назначение экипажа на рейса
        {
            ViewBag.AllCrewMembers = crewMember.GetAll();
            if (ModelState.IsValid)
            {
                List<CrewMember> crew = new List<CrewMember>()
                {
                    crewMember.Get(assign.PilotId),
                    crewMember.Get(assign.NavigatorId),
                    crewMember.Get(assign.RadioOperatorId),
                    crewMember.Get(assign.StewardessId)
                };
                Flight flight = flightMethods.Get(assign.Id);
                flight.CrewMembers = crew;
                flightMethods.CrewAssigment(flight);
                logger.Info($"For Flight: Id: {flight.Id} assigned crew By: {HttpContext.User.Identity.Name}");
                return RedirectToAction("DispatcherPanel", "Dispatcher");
            }
            else
                return View(assign);
        }
        public ActionResult Ready(int id) //управление статусом на ready для рейса
        {
            flightMethods.Ready(id);
            logger.Info($"Flight Id {id} get status READY By: {HttpContext.User.Identity.Name}");
            return RedirectToAction("AllFlightsDispatcher", "Dispatcher");
        }
        public ActionResult Unready(int id) //управление статусом на unready для рейса
        {
            flightMethods.Unready(id);
            logger.Info($"Flight Id {id} get status UNREADY By: {HttpContext.User.Identity.Name}");
            return RedirectToAction("AllFlightsDispatcher", "Dispatcher");
        }
        public ActionResult More(int id) //отображение полной информации о рейсе
        {
            int? check = id;
            if (check != null & check > 0)
            {
                return View(flightMethods.Get(id));
            }
            else
                return View("NotFound");
        }
        [HttpGet]
        public ActionResult EditCrew()
        {
            ViewBag.AllCrewMembers = crewMember.GetAll();
            return View();
        }
        [HttpPost]
        public ActionResult EditCrew(AssignCrewViewModel assign)
        {
            ViewBag.AllCrewMembers = crewMember.GetAll();
            Flight flight = flightMethods.Get(assign.Id);
            flight.CrewMembers.Clear();
            flightMethods.Edit(flight);
            if (ModelState.IsValid)
            {
                List<CrewMember> crew = new List<CrewMember>()
                {
                    crewMember.Get(assign.PilotId),
                    crewMember.Get(assign.NavigatorId),
                    crewMember.Get(assign.RadioOperatorId),
                    crewMember.Get(assign.StewardessId)
                };
                flight.CrewMembers = crew;
                flightMethods.CrewAssigment(flight);
                logger.Info($"Flight Id: {flight.Id} crew edited By: {HttpContext.User.Identity.Name}");
                return RedirectToAction("DispatcherPanel", "Dispatcher");
            }
            else
                return View(assign);
        }
        public ActionResult SortDescendingId(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderByDescending(x => x.Id).Where(x => x.IsDeleted == true).Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<Flight> flightsCount = flightMethods.GetAll().Where(x => x.IsDeleted == true);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightsCount.Count() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        public ActionResult SortAscendingId(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderBy(x => x.Id).Where(x => x.IsDeleted == true).Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<Flight> flightsCount = flightMethods.GetAll().Where(x => x.IsDeleted == true);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightsCount.Count() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        public ActionResult SortDescendingPrice(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderByDescending(x => x.Price).Where(x => x.IsDeleted == true).Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<Flight> flightsCount = flightMethods.GetAll().Where(x => x.IsDeleted == true);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightsCount.Count() }; ;
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        public ActionResult SortAscendingPrice(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderBy(x => x.Price).Where(x => x.IsDeleted == true).Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<Flight> flightsCount = flightMethods.GetAll().Where(x => x.IsDeleted == true);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightsCount.Count() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
    }
}