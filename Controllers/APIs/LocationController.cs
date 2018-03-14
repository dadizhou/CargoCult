using CargoCult.Data;
using CargoCult.ExtensionClasses;
using CargoCult.Models;
using CargoCult.HelperClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Spatial;
using System.Collections.Generic;
using System.Linq;

namespace CargoCult.Controllers.APIs
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private IMainDBRepository repository;
        private ILocationSearch locationSearch;
        public LocationController(IMainDBRepository repo, ILocationSearch loc)
        {
            repository = repo;
            locationSearch = loc;
        }

        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Location> GetAllLocations()
        {
            return repository.Locations.ToList();
        }

        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Location> SearchByRadius([FromQuery]double latitude, [FromQuery]double longitude, [FromQuery]double searchRadius)
        {
            List<Location> locations = new List<Location>();
            if (latitude != 0 && longitude != 0 && searchRadius != 0)
            {
                var baseLocation = new Location
                {
                    Title = "Base Location",
                    Description = "Base location lat lon",
                    Latitude = latitude,
                    Longitude = longitude
                };

                locations.Add(baseLocation);

                locations.AddRange(locationSearch.Locations(baseLocation, (double)searchRadius));
            }

            return locations;
        }
    }
}