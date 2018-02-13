using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CargoCult.Models;

namespace CargoCult.Data.Seeding
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

            SeedStoredProcedure.InitialiseStoredProcedure(app);

            SeedTemplateData.InitialiseTemplateData(app);
        }
    }
}