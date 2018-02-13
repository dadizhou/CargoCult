using CargoCult.Data;
using CargoCult.ExtensionClasses;
using CargoCult.Models;
using CargoCult.HelperClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Spatial;
using System.Collections.Generic;
using System.Linq;

namespace CargoCult.Controllers
{
    public class HomeController : Controller
    {
        private IMainDBRepository repository;
        private ILocationSearch locationSearch;
        public HomeController(IMainDBRepository repo, ILocationSearch loc)
        {
            repository = repo;
            locationSearch = loc;
        }

        public ActionResult Index()
        {
            var latitude = TempData["Latitude"];
            var longitude = TempData["Longitude"];
            var searchRadius = TempData["SearchRadius"];

            if (latitude != null && longitude != null && searchRadius != null)
            {
                List<Location> viewLocations = new List<Location>();

                var currentLocation = new Location
                {
                    Title = "Current Location",
                    Description = "Current location lat lon",
                    Latitude = (double)latitude,
                    Longitude = (double)longitude
                };

                viewLocations.Add(currentLocation);

                viewLocations.AddRange(locationSearch.Locations(currentLocation, (double)searchRadius));

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