using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Projects.EFCore.Data;
using Projects.Models;
using Projects.Helper;
using Microsoft.EntityFrameworkCore;
using Projects.EFCore.Models;
using Newtonsoft.Json;

namespace Projects.Services
{
    class ForecastService : RestAPIClient
    {
        private readonly IConfiguration _configuration;
        public IConfigurationSection serviceConfigurations;
        public string ProjectsConnectionString = string.Empty;

        public ForecastService(IConfiguration configuration)
        {

            _configuration = configuration;

            if (configuration != null)
            {

                serviceConfigurations = _configuration.GetSection("AccuWeatherAPIs");
                ProjectsConnectionString = configuration.GetConnectionString("ProjectsDB");
            }


        }

        public ForecastRootObject GetForecastAsync(string cityCode, string days)
        {

            ForecastRootObject forecastResults = new ForecastRootObject();

            string key = serviceConfigurations.GetValue<string>("key");
            string baseAddress = serviceConfigurations.GetValue<string>("baseAddress");
            string version = serviceConfigurations.GetSection("forecast").GetValue<string>("version");
            string uri = string.Concat("forecasts/", version, "/daily/", days, "day/");

            string apiAddress = baseAddress;
            string parameters = String.Concat("/", uri, cityCode, "?apikey=", key);

            if(days == "1") { 
                forecastResults = GetSingleRestAPIClient<ForecastRootObject>(apiAddress, parameters);

                using (ProjectsContext dbContext = new ProjectsContext(new DbContextOptionsBuilder<ProjectsContext>().UseSqlServer(ProjectsConnectionString).Options))
                {
                    var forecastResult = new CityForecast();
                    forecastResult.RecordEntryDate = DateTime.UtcNow;
                    forecastResult.RecordLastDate = DateTime.UtcNow;
                    forecastResult.RecordStatus = "Live";
                    forecastResult.Json = JsonConvert.SerializeObject(forecastResults);
                    dbContext.CityForecasts.Add(forecastResult);
                    dbContext.SaveChanges();


                   //TO DO PosDTO return to console

                }

            }
            return forecastResults;

        }
    }
}
