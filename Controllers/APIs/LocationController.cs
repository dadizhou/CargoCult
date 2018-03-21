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
        [HttpGet("{locationID}", Name = "[action]")]
        public IActionResult GetLocationByID(long locationID)
        {
            var location = repository.Locations.FirstOrDefault(t => t.LocationID == locationID);
            if (location == null)
            {
                return NotFound();
            }
            return new ObjectResult(location);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Create([FromBody] Location location)
        {
            if (location == null)
            {
                return BadRequest();
            }

            repository.SaveLocation(location);

            return CreatedAtRoute("GetLocationByID", new { locationID = location.LocationID }, location);
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult Update([FromBody] Location location)
        {
            if (location == null)
            {
                return BadRequest();
            }

            var locationInDB = repository.Locations.FirstOrDefault(l => l.LocationID == location.LocationID);
            if (locationInDB == null)
            {
                return NotFound();
            }

            repository.SaveLocation(location);
            return new NoContentResult();
        }

        //[Route("[action]")]
        //[HttpPut("{locationID}")]
        //public IActionResult Update(long locationID, [FromBody] Location location)
        //{
        //    if (location == null || location.LocationID != locationID)
        //    {
        //        return BadRequest();
        //    }

        //    var locationInDB = repository.Locations.FirstOrDefault(l => l.LocationID == locationID);
        //    if (locationInDB == null)
        //    {
        //        return NotFound();
        //    }

        //    repository.SaveLocation(location);
        //    return new NoContentResult();
        //}

        [Route("[action]")]
        [HttpDelete("{locationID}")]
        public IActionResult Delete(long locationID)
        {
            Location deletedLocation = repository.DeleteLocation(locationID);
            if (deletedLocation == null)
            {
                return NotFound();
            }
            return new NoContentResult();
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