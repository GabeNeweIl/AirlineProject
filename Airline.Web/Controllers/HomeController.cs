using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airline.BLL.Interfaces;
using Airline.Models.Models;
using Airline.Web.Models;

namespace Airline.Web.Controllers
{
    public class HomeController : Controller
    {
        IFlightMethods flightMethods;
        IProfileMethods ProfileMethods;
        public HomeController(IFlightMethods _flight, IProfileMethods _profileMethods)
        {
            ProfileMethods = _profileMethods;
            flightMethods = _flight;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult NoResults()
        {
            return View();
        }
        public ActionResult SearchFailed()
        {
            return View();
        }
        public ActionResult Search(Search search) //полный поиск по рейсам
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Flight flight = flightMethods.GetAll().First(x => x.FromCity.ToLower() == search.From.ToLower() &&
                    x.ToCity.ToLower() == search.To.ToLower() &&
                    x.Departure.ToShortDateString() == search.DateDeparture.ToShortDateString());
                    if (User.IsInRole("admin"))
                        return RedirectToAction("More", "Admin", new { Id = flight.Id });
                    if (User.IsInRole("dispatcher"))
                        return RedirectToAction("More", "Dispatcher", new { Id = flight.Id });
                    if (User.IsInRole("user"))
                        return RedirectToAction("More", "Home", new { id = flight.Id });
                    else
                        return RedirectToAction("SearchFailed", "Home");
                }
                catch (InvalidOperationException)
                {
                    return RedirectToAction("SearchFailed", "Home");
                }
            }
            else
                return RedirectToAction("SearchFailed", "Home");
        }
        public ActionResult SearchByNumber(SearchByNumber search) //поиск рейса по номеру
        {
            if (ModelState.IsValid)
            {
                if (search.Number > 0 && flightMethods.Get(search.Number) != null)
                {
                    if (User.IsInRole("admin"))
                        return RedirectToAction("More", "Admin", new { Id = flightMethods.Get(search.Number).Id });
                    if (User.IsInRole("dispatcher"))
                        return RedirectToAction("More", "Dispatcher", new { Id = flightMethods.Get(search.Number).Id });
                    if (User.IsInRole("user"))
                        return RedirectToAction("More", "Home", new { Id = flightMethods.Get(search.Number).Id });
                    else
                        return RedirectToAction("SearchFailed", "Home");
                }
                else
                    return RedirectToAction("SearchFailed", "Home");
            }
            else
                return RedirectToAction("SearchFailed", "Home");
        }
        public ActionResult SortDescendingId(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderByDescending(x => x.Id).Where(x => x.Departure > DateTime.Now).Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<Flight> flightsCount = flightMethods.GetAll().Where(x => x.Departure > DateTime.Now);
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
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderBy(x => x.Id).Where(x => x.Departure > DateTime.Now).Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<Flight> flightsCount = flightMethods.GetAll().Where(x => x.Departure > DateTime.Now);
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
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderByDescending(x => x.Price).Where(x => x.Departure > DateTime.Now).Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<Flight> flightsCount = flightMethods.GetAll().Where(x => x.Departure > DateTime.Now);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightsCount.Count() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        public ActionResult SortAscendingPrice(int page=1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().OrderBy(x => x.Price).Where(x => x.Departure > DateTime.Now).Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<Flight> flightsCount = flightMethods.GetAll().Where(x => x.Departure > DateTime.Now);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightsCount.Count() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        public ActionResult AllFlights(int page = 1)
        {
            int pageSize = 3;
            IEnumerable<Flight> flightsPerPage = flightMethods.GetAll().Where(x => x.Departure > DateTime.Now).Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<Flight> flightsCount = flightMethods.GetAll().Where(x => x.Departure > DateTime.Now);
            Page pages = new Page { PageNumber = page, PageSize = pageSize, TotalItems = flightsCount.Count() };
            PageViewMdodel pageView = new PageViewMdodel { Page = pages, Flights = flightsPerPage };
            if (flightsPerPage.Any())
                return View(pageView);
            else
                return RedirectToAction("NotFound", "Home");
        }
        [Authorize(Roles = "user")]
        public ActionResult Book(int id) //бронирование рейса для пользователя
        {
            try
            {
                int profileId = ProfileMethods.GetByEmail(User.Identity.Name);
                Profile profile = ProfileMethods.Get(profileId);
                Flight flight = flightMethods.Get(id);
                if (profile.Name != null && profile.Surname != null && profile.PassportID != null && profile.Birth != null && profile.Email != null 
                    && flight.TotalNumberPassengers != flight.CurrentNumberPassengers)
                {
                    if (!profile.Flights.Contains(flight))
                    {
                    List<Flight> flights = new List<Flight>()
                    {
                        flight
                    };
                        profile.Flights = flights;
                        ProfileMethods.Edit(profile);
                        flight.CurrentNumberPassengers++;
                        flightMethods.Edit(flight);
                    return RedirectToAction("UserBooking", "Home");
                    }
                    else
                        return RedirectToAction("UserBooking", "Home");

                }
                else
                    return RedirectToAction("FillProfile", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("FillProfile", "Home");
            }   
        }
        [Authorize(Roles ="user")]
        public ActionResult UserBooking() //отображение всех забронированых рейсов для пользователя
        {
            int id = ProfileMethods.GetByEmail(User.Identity.Name);
            Profile profile = ProfileMethods.Get(id);
            return View(profile);
        }
        [Authorize(Roles ="user")]
        [HttpGet]
        public ActionResult FillProfile()
        {
            return View();
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult FillProfile(ProfileViewModel _profile) //если у пользователя не заполнен профиль перенапрвляет на это действие
        {
            if (ModelState.IsValid && _profile.Birth < DateTime.Now)
            {
                Profile profile = new Profile
                {
                    Email = User.Identity.Name,
                    Name = _profile.Name,
                    Surname = _profile.Surname,
                    PassportID = _profile.PassportID,
                    Birth = _profile.Birth
                };
                ProfileMethods.Create(profile);
                return RedirectToAction("ProfileView", "Home");
            }
            else
                return View(_profile);
        }
        [Authorize(Roles = "user")]
        public ActionResult ProfileView() //отображение информации о пользователе
        {
            try
            {
                int id = ProfileMethods.GetByEmail(User.Identity.Name);
                Profile profile = ProfileMethods.Get(id);
                return View(profile);
            }
            catch (Exception)
            {
                return RedirectToAction("FillProfile", "Home");
            }
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult UserEdit() 
        {
            return View();
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult UserEdit(ProfileViewModel _profile) //редактирование профиля
        {
            if (ModelState.IsValid && _profile.Birth < DateTime.Now)
            {
                int id = ProfileMethods.GetByEmail(User.Identity.Name);
                Profile profile = ProfileMethods.Get(id);
                profile.Email = User.Identity.Name;
                profile.Name = _profile.Name;
                profile.Surname = _profile.Surname;
                profile.PassportID = _profile.PassportID;
                profile.Birth = _profile.Birth;
                ProfileMethods.Edit(profile);
                return RedirectToAction("ProfileView", "Home");
            }
            else
                return View(_profile);
        }
    }
}