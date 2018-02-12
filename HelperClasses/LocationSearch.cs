using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CargoCult.Data;
using CargoCult.Models;
using CargoCult.ExtensionClasses;
using System.Linq;

namespace CargoCult.HelperClasses
{
    public class LocationSearch : ILocationSearch
    {
        private IMainDBRepository repository;
        public LocationSearch(IMainDBRepository repo)
        {
            repository = repo;
        }

        public List<Location> Locations (Location currentLocation, double searchRadius)
        {
            return (from l in repository.Locations where l.Position.Distance(currentLocation.Position) < searchRadius select l).ToList();
        }

    }
}
