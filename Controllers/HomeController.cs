using Microsoft.AspNetCore.Mvc;
using CargoCult.Models;
using System.Collections.Generic;

namespace CargoCult.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            List<Location> locations = new List<Location>
            {
                new Location {
                    Name = "Melbourne CBD",
                    Lat = -37.813611,
                    Lng = 144.963056
                },
                new Location {
                    Name = "Flagstaff Gardens",
                    Lat = -37.8111635,
                    Lng = 144.9544162
                }
            };

            ViewBag.Locations = locations;
            return View();
        }
    }
}