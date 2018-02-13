using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CargoCult.Models;

namespace CargoCult.Data.Seeding
{
    public static class SeedStoredProcedure
    {
        public static void InitialiseStoredProcedure(IApplicationBuilder app)
        {
            MainDBContext context = app.ApplicationServices.GetRequiredService<MainDBContext>();

            var sp_SearchByRadius_Text = 
            "CREATE PROCEDURE [dbo].[SearchByRadius] " +
                "@latitude float, @longitude float, @searchRadius float " +
            "AS " +
                "DECLARE @point geography = geography::STGeomFromText('POINT(' + CAST(@longitude AS VARCHAR(20)) + ' ' + CAST(@latitude AS VARCHAR(20)) +')', 4326); " +
                "SELECT [LocationID], " +
                        "[Title], " +
                        "[Description], " +
                        "[Latitude], " +
                        "[Longitude] " +
                "FROM [dbo].[Location] " +
                "WHERE @point.STDistance(geography::STGeomFromText('POINT(' + CAST([Longitude] AS VARCHAR(20)) + ' ' + CAST([Latitude] AS VARCHAR(20)) + ')', 4326)) <= @searchRadius * 1000";

            context.Database.ExecuteSqlCommand(sp_SearchByRadius_Text);
        }
    }
}