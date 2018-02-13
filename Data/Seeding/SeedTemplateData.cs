using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CargoCult.Models;

namespace CargoCult.Data.Seeding
{
    public static class SeedTemplateData
    {
        public static void InitialiseTemplateData(IApplicationBuilder app)
        {
            MainDBContext context = app.ApplicationServices.GetRequiredService<MainDBContext>();

            var locations = new Location[]
            {
                new Location
                { Title = "Melbourne CBD", Description = "CBD of Melbourne", Latitude = -37.813611, Longitude = 144.963056 },
                new Location
                { Title = "Flagstaff Gardens", Description = "Haunted Gardens", Latitude = -37.8111635, Longitude = 144.9544162 },
                new Location
                { Title = "Box Hill", Description = "Overpriced Grocery", Latitude = -37.8204789, Longitude = 145.1220558 },
                new Location
                { Title = "Camberwell", Description = "Sunday Market", Latitude = -37.8363944, Longitude = 145.0752454 },
                new Location
                { Title = "Ringwood", Description = "Termites Zone", Latitude = -37.8099888, Longitude = 145.2213589 },
                new Location
                { Title = "Federation Square", Description = "The Feds", Latitude = -37.8185448, Longitude = 144.9682679 }
            };
            foreach (Location l in locations)
            {
                context.Locations.Add(l);
            }
            context.SaveChanges();
        }
    }
}