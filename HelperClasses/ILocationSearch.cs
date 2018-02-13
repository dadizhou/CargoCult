using System.Linq;
using System.Collections.Generic;
using CargoCult.Models;

namespace CargoCult.HelperClasses
{
    public interface ILocationSearch
    {
        List<Location> Locations(Location currentLocation, double searchRadius);
    }
}