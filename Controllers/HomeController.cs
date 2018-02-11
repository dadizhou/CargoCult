using Microsoft.AspNetCore.Mvc;
using CargoCult.Models;
using System.Collections.Generic;
using Microsoft.Spatial;
using CargoCult.Data;
using System.Linq;
using CargoCult.ExtensionClasses;
using Microsoft.AspNetCore.Http;

namespace CargoCult.Controllers
{
    public class HomeController : Controller
    {
        private IMainDBRepository repository;
        public HomeController(IMainDBRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            //List<Location> locations = new List<Location>
            //{
            //    new Location {
            //        Name = "Melbourne CBD",
            //        Position = GeographyPoint.Create(-37.813611,144.963056)
            //    },
            //    new Location {
            //        Name = "Flagstaff Gardens",
            //        Position = GeographyPoint.Create(-37.8111635,144.9544162)
            //    }
            //};
            //ViewBag.Locations = locations;
            var latitude = TempData["Latitude"];
            var longitude = TempData["Longitude"];
            var searchRadius = TempData["SearchRadius"];

            if (latitude != null && longitude != null && searchRadius != null)
            {
                List<Location> viewLocations = new List<Location>();

                var currentLocation = new Location
                {
                    Name = "current location",
                    Latitude = (double)latitude,
                    Longitude = (double)longitude
                };

                viewLocations.Add(currentLocation);

                viewLocations.AddRange(from l in repository.Locations where l.Position.Distance(currentLocation.Position) < (double)searchRadius select l);

                ViewBag.Locations = viewLocations;
                ViewBag.SearchRadius = searchRadius;
            }

            return View();
        }

        [HttpPost]
        public ActionResult SearchByRadius(double latitude, double longitude, double searchRadius)
        {
            TempData["Latitude"] = latitude;
            TempData["Longitude"] = longitude;
            TempData["SearchRadius"] = searchRadius;

            return Json(new { redirectToUrl = Url.Action("Index", "Home") }); ;
        }
    }
}