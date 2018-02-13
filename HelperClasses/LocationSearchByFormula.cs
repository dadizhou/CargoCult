using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CargoCult.Data;
using CargoCult.Models;
using CargoCult.ExtensionClasses;

namespace CargoCult.HelperClasses
{
    public class LocationSearchByFormula : ILocationSearch
    {
        private IMainDBRepository repository;
        public LocationSearchByFormula(IMainDBRepository repo)
        {
            repository = repo;
        }

        public List<Location> Locations (Location currentLocation, double searchRadius)
        {
            return (from l in repository.Locations where l.Position.Distance(currentLocation.Position) < searchRadius select l).ToList();
        }

    }
}
