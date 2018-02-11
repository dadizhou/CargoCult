using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CargoCult.Models;

namespace CargoCult.Data
{
    public static class SeedData
    {
        public static void InitialiseDatabase(IApplicationBuilder app)
        {
            MainDBContext context = app.ApplicationServices.GetRequiredService<MainDBContext>();

            context.Database.Migrate();

            if (context.Locations.Any())
            {
                return;
            }

            var locations = new Location[]
            {
                new Location
                { Name = "Melbourne CBD", Latitude = -37.813611, Longitude = 144.963056 },
                new Location
                { Name = "Flagstaff Gardens", Latitude = -37.8111635, Longitude = 144.9544162 },
                new Location
                { Name = "Box Hill", Latitude = -37.8204789, Longitude = 145.1220558 },
                new Location
                { Name = "Camberwell", Latitude = -37.8363944, Longitude = 145.0752454 },
                new Location
                { Name = "Ringwood", Latitude = -37.8099888, Longitude = 145.2213589 },
                new Location
                { Name = "Federation Square", Latitude = -37.8185448, Longitude = 144.9682679 }
            };
            foreach (Location l in locations)
            {
                context.Locations.Add(l);
            }
            context.SaveChanges();
        }
    }
}