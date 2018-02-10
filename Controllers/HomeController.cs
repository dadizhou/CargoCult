using Microsoft.AspNetCore.Mvc;
using CargoCult.Models;
using System.Collections.Generic;
using Microsoft.Spatial;

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
                    Position = GeographyPoint.Create(-37.813611,144.963056)
                },
                new Location {
                    Name = "Flagstaff Gardens",
                    Position = GeographyPoint.Create(-37.8111635,144.9544162)
                }
            };

            ViewBag.Locations = locations;
            return View();
        }
        [HttpPost]
        public ActionResult GetCurrentLocation(string latitude, string longitude)
        {

            return null;
        }
    }
}