using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airline.Models.Models;
using Airline.BLL.Interfaces;
using Airline.BLL.Methods;
using Airline.Web.Models;
using NLog;
using System.IO;
using System.Web.Hosting;

namespace Airline.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ICrewMemberMethods crewMember;
        IFlightMethods flightMethods;
        IRequestMethods requestMethods;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public AdminController(ICrewMemberMethods _crew, IFlightMethods _flight, IRequestMethods _request)
        {
            crewMember = _crew;
            flightMethods = _flight;
            requestMethods = _request;
        }
        public ActionResult AdminPanel()
        {
            return View("AdminPanel");
        }
        public ActionResult AllCrewMembers()
        {
            return View("AllCrewMembers",crewMember.GetAll());
        }
        [HttpGet]
        public ActionResult NewCrewMember()
        {
            return View("NewCrewMember");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewCrewMember(NewCrewMemberViewModel newCrew)
        {
            if (ModelState.IsValid && newCrew.Age <= 55 && newCrew.Age >= 18) //проверка корректности ввода
            {
                CrewMember crew = new CrewMember
                {
                    Name = newCrew.Name,
                    Surname = newCrew.Surname,
                    Position = newCrew.Position,
                    Age = newCrew.Age
                };
                crewMember.Create(crew);
                logger.Info($"Created crew member: Name: {crew.Name}, Surname: {crew.Surname}, Position: {crew.Position}," + //логирование создания члена экипажа
                    $" Age: {crew.Age} By: {HttpContext.User.Identity.Name}");
                return RedirectToAction("AllCrewMembers", "Admin");
            }
            else
                ModelState.AddModelError("", "Fill in all gaps or fill correct 'Age' (must be between 18 and 55");
            return View(newCrew);
        }
        [HttpGet]
        public ActionResult HideCrewMember(int id) // скрытие экипажа IsDeleted = false 
        {
            int? check = id;
            if (check != null && id > 0)
            {
                CrewMember crew = crewMember.Get(id);
                crewMember.SoftDelete(id);
                logger.Info($"Hidden crew member:Id {crew.Id}, Name: {crew.Name}, Surname: {crew.Surname}," + //логирование скрытия члена экипажа
                    $" Position: {crew.Position}, Age: {crew.Age} By: {HttpContext.User.Identity.Name}");
                return RedirectToAction("AllCrewMembers", "Admin");
            }
            else
                return RedirectToAction("AllCrewMembers", "Admin");
        }
        public ActionResult DeleteCrewMember(int id) //полное удаление из бд
        {
            int? check = id;
            if (check != null && id > 0)
            {
                CrewMember crew = crewMember.Get(id);
                crewMember.Delete(id);
                logger.Info($"Deleted crew member: Name: {crew.Name}, Surname: {crew.Surname}," +  //логирование полного удаление из бд члена экипажа
                    $" Position: {crew.Position}, Age: {crew.Age} By: {HttpContext.User.Identity.Name}");
                return RedirectToAction("AllCrewMembers", "Admin");
            }
            else
                return RedirectToAction("AllCrewMembers", "Admin");
        }
        [HttpGet]
        public ActionResult RestoreCrewMember(int id) //восстановление IsDeleted = true
        {
            int? check = id;
            if (check != null)
            {
                CrewMember crew = crewMember.Get(id);
                crewMember.Restore(id);
                logger.Info($"Restored crew member:Id {crew.Id}, Name: {crew.Name}, Surname: {crew.Surname}," +
                    $" Position: {crew.Position}, Age: {crew.Age} By: {HttpContext.User.Identity.Name}");
                return RedirectToAction("AllCrewMembers", "Admin");
            }
            else
                return RedirectToAction("AllCrewMembers", "Admin");
        }
        [HttpGet]
        public ActionResult EditCrewMember()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditCrewMember(EditCrewMemberViewModel _crew)
        {
            if (_crew.Id > 0)
            {
                if (ModelState.IsValid && _crew.Age <= 55 && _crew.Age >= 18)
                {
                    CrewMember crew = crewMember.Get(_crew.Id);
                    crew.Position = _crew.Position;
                    crew.Name = _crew.Name;
                    crew.Surname = _crew.Surname;
                    crew.Age = _crew.Age;
                    crewMember.Edit(crew);
                    return RedirectToAction("AllCrewMembers", "Admin");
                }
                else
                    return View(_crew);
            }
            else
                return View("NotFound");
        }
        public ActionResult AllFlights(int page=1)
        {
            int pageSize = 3; //последующие методы для отображения всех рейсов с пагианцией
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightMethods.GetCount() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home"); //если бд пустая следутет закоментировать строчки 140 142 143

        }
        [HttpGet]
        public ActionResult NewFlight()
        {
            return View("NewFlight");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewFlight(NewFlightViewModel _flight)
        {
            if (ModelState.IsValid)
            {
                if (_flight.Arraival > DateTime.Now && _flight.Departure > DateTime.Now && _flight.Arraival >= _flight.Departure
                    && _flight.Price > 0 && _flight.Total > 0 && _flight.Total < 100)
                {
                    Flight flight = new Flight
                    {
                        Departure = _flight.Departure,
                        Arrival = _flight.Arraival,
                        FromCountry = _flight.FromCountry,
                        FromCity = _flight.FromCity,
                        ToCountry = _flight.ToCountry,
                        ToCity = _flight.ToCity,
                        Price = _flight.Price,
                        TotalNumberPassengers = _flight.Total
                    };
                    flightMethods.Create(flight);
                    logger.Info($"Created fligth: Departure: {flight.Departure}, Arrival: {flight.Arrival}, FCity: {flight.FromCity}," +
                        $"FCountry: {flight.FromCountry}, TCity: {flight.ToCity}, TCountry: {flight.ToCountry}, By: {HttpContext.User.Identity.Name}");
                    return RedirectToAction("AllFlights", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Check inptu data. Date must in future. Max passanger must be less then 100");
                    ModelState.IsValidField("");
                    return View(_flight);
                }

            }
            else
                ModelState.AddModelError("", "Fill in all gaps, Date must be in future");
            return View(_flight);
        }
        [HttpGet]
        public ActionResult EditFlight()
        {
            return View("EditFlight");
        }
        [HttpPost]
        public ActionResult EditFlight(EditFlightViewModel _flight)
        {
            if (_flight.Id > 0)
            {
                if (_flight.Arraival > DateTime.Now && _flight.Departure > DateTime.Now && _flight.Price > 0 && _flight.Total > 0
                    && _flight.Arraival >= _flight.Departure)
                {
                    Flight flight = flightMethods.Get(_flight.Id);
                    flight.Departure = _flight.Departure;
                    flight.Arrival = _flight.Arraival;
                    flight.FromCountry = _flight.FromCountry;
                    flight.FromCity = _flight.FromCity;
                    flight.ToCountry = _flight.ToCountry;
                    flight.ToCity = _flight.ToCity;
                    flight.Price = _flight.Price;
                    flight.TotalNumberPassengers = _flight.Total;
                    flightMethods.Edit(flight);
                    logger.Info($"Edited fligth: Id: {flight.Id}, Departure: {flight.Departure.ToShortDateString()}, Arrival: {flight.Arrival.ToShortDateString()}, FCity: {flight.FromCity}," +
                        $"FCountry: {flight.FromCountry}, TCity: {flight.ToCity}, TCountry: {flight.ToCountry}, By: {HttpContext.User.Identity.Name}");
                    return RedirectToAction("AllFlights", "Admin");
                }
                else
                    return View(_flight);
            }
            else
                return View("Error404");
        }
        public ActionResult HideFlight(int id)
        {
            int? check = id;
            if (check != null && id > 0)
            {
                Flight flight = flightMethods.Get(id);
                flightMethods.SoftDelete(id);
                logger.Info($"Hidden fligth: Id: {flight.Id}, Departure: {flight.Departure.ToShortDateString()}, Arrival: {flight.Arrival.ToShortDateString()}, FCity: {flight.FromCity}," +
                        $"FCountry: {flight.FromCountry}, TCity: {flight.ToCity}, TCountry: {flight.ToCountry}, By: {HttpContext.User.Identity.Name}");
                return RedirectToAction("AllFlights", "Admin");
            }
            else
                return RedirectToAction("AllFlights", "Admin");
        }
        public ActionResult DeleteFlight(int id)
        {
            int? check = id;
            if (check != null && id > 0)
            {
                Flight flight = flightMethods.Get(id);
                flightMethods.Delete(id);
                logger.Info($"Deleted fligth: Id: {flight.Id}, Departure: {flight.Departure.ToShortDateString()}, Arrival: {flight.Arrival.ToShortDateString()}, FCity: {flight.FromCity}," +
                        $"FCountry: {flight.FromCountry}, TCity: {flight.ToCity}, TCountry: {flight.ToCountry}, By: {HttpContext.User.Identity.Name}");
                return RedirectToAction("AllFlights", "Admin");
            }
            else
                return RedirectToAction("AllFlights", "Admin");
        }
        public ActionResult RestoreFlight(int id)
        {
            int? check = id;
            if (check != null && id > 0)
            {
                Flight flight = flightMethods.Get(id);
                flightMethods.Restore(id);
                logger.Info($"Restored fligth: Id: {flight.Id}, Departure: {flight.Departure.ToShortDateString()}, Arrival: {flight.Arrival.ToShortDateString()}, FCity: {flight.FromCity}," +
                        $"FCountry: {flight.FromCountry}, TCity: {flight.ToCity}, TCountry: {flight.ToCountry}, By: {HttpContext.User.Identity.Name}");
                return RedirectToAction("AllFlights", "Admin");
            }
            else
                return RedirectToAction("AllFlights", "Admin");
        }
        public ActionResult AllRequests()
        {
            return View(requestMethods.GetAll());
        }
        public ActionResult RequestStatusDone(int id)
        {
            int? check = id;
            if (check != null && id > 0)
            {
                requestMethods.StatusDone(id);
                return RedirectToAction("AllRequests", "Admin");
            }
            else
                return RedirectToAction("AllRequests", "Admin");
        }
        public ActionResult RequestStatusReject(int id)
        {
            int? check = id;
            if (check != null && id > 0)
            {
                requestMethods.StatusReject(id);
                return RedirectToAction("AllRequests", "Admin");
            }
            else
                return RedirectToAction("AllRequests", "Admin");
        }
        [HttpGet]
        public ActionResult More(int id)
        {
            int? check = id;
            if (check != null & check > 0)
            {
                return View(flightMethods.Get(id));
            }
            else
                return View("NotFound");
        }
        public ActionResult SortDescendingId(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightMethods.GetCount() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        public ActionResult SortAscendingId(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightMethods.GetCount() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        public ActionResult SortDescendingPrice(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderByDescending(x => x.Price).Skip((page - 1) * pageSize).Take(pageSize);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightMethods.GetCount() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        public ActionResult SortAscendingPrice(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderBy(x => x.Price).Skip((page - 1) * pageSize).Take(pageSize);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightMethods.GetCount() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        public ActionResult SortByCountryAndCity(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderBy(x => x.FromCountry).ThenBy(x => x.FromCity)
                .Skip((page - 1) * pageSize).Take(pageSize);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightMethods.GetCount() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        public ActionResult AllLogs() // чтения лога из тхт файла
        {
            string path = HostingEnvironment.MapPath("/logs/Logs.log");
            List<string> logs = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    logs.Add(line);
                }

            }
            return View(logs);
        }
        [HttpPost]
        public ActionResult SearchLog(string search) //поиск по логам
        {
            string path = HostingEnvironment.MapPath("/logs/Logs.log");
            List<string> logs = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(search))
                        logs.Add(line);
                }

            }
            return View("AllLogs",logs);
        }
    }
}