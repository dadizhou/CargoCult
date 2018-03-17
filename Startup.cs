using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CargoCult.Data;
using CargoCult.Data.Seeding;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CargoCult.HelperClasses;
using Microsoft.AspNetCore.Cors;

namespace CargoCult
{
    public class Startup
    {
        public IConfiguration Config { get; }
        public Startup(IConfiguration config) => Config = config;

        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<MainDBContext>(options => options.UseSqlServer(Config.GetConnectionString("MyDbConnection")));
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                        builder => builder.WithOrigins("https://cargocultaccess.azurewebsites.net"));
                });
            }
            else
            {
                services.AddDbContext<MainDBContext>(options => options.UseSqlServer(Config["Data:CargoCult:ConnectionString"]));
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                        builder => builder
                                    .AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials()
                                    );
                });
            }

            services.AddTransient<IMainDBRepository, MainDBRepository>();
            services.AddTransient<ILocationSearch, LocationSearchBySTDistance>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowSpecificOrigin");
            app.UseMvcWithDefaultRoute();
            SeedData.InitialiseDatabase(app);
        }
    }
}
