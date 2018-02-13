using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CargoCult.Data;
using CargoCult.Models;
using CargoCult.ExtensionClasses;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace CargoCult.HelperClasses
{
    public class LocationSearchBySTDistance : ILocationSearch
    {
        private MainDBContext context;
        public LocationSearchBySTDistance(MainDBContext c)
        {
            context = c;
        }

        public List<Location> Locations(Location currentLocation, double searchRadius)
        {
            var latitudeParam = new SqlParameter("latitude", currentLocation.Latitude);
            var longitudeParam = new SqlParameter("longitude", currentLocation.Longitude);
            var searchRadiusParam = new SqlParameter("searchRadius", searchRadius);

            var result = context.Locations
                .FromSql
                (
                    "EXECUTE dbo.SearchByRadius @latitude, @longitude, @searchRadius",
                    latitudeParam,
                    longitudeParam,
                    searchRadiusParam
                )
                .ToList();

            return result;
        }

    }
}
