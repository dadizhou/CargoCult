using System.Collections.Generic;
using System.Linq;
using CargoCult.Models;

namespace CargoCult.Data
{
    public class MainDBRepository : IMainDBRepository
    {
        private MainDBContext context;

        public MainDBRepository(MainDBContext c)
        {
            context = c;
        }

        public IQueryable<Location> Locations => context.Locations;

        public void SaveLocation(Location location)
        {
            if (location.LocationID == 0)
            {
                context.Locations.Add(location);
            }
            else
            {
                Location locationInDB = context.Locations.FirstOrDefault(l => l.LocationID == location.LocationID);
                if (locationInDB != null)
                {
                    locationInDB.Title = location.Title;
                    locationInDB.Description = location.Description;
                    locationInDB.Latitude = location.Latitude;
                    locationInDB.Longitude = location.Longitude;
                }
            }
            context.SaveChanges();
        }

        public Location DeleteLocation(long locationID)
        {
            Location locationInDB = context.Locations.FirstOrDefault(l => l.LocationID == locationID);
            if (locationInDB != null)
            {
                context.Locations.Remove(locationInDB);
                context.SaveChanges();
            }
            return locationInDB;
        }
    }
}