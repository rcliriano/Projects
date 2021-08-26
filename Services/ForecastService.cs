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
            
            /*
             * uri is typically refered to as the whole request. so baseaddress/apiaddress?parameters
             * instead of using string.Concat you can store the api address as /forecasts/{0}/daily/{1}day/{2} and use string.format to replace the {x} values.
             * -NB
             */
            string uri = string.Concat("forecasts/", version, "/daily/", days, "day/");
            /*
             * why duplicate variable on line 59?
             * parameters should be anything after the ? in the uri. 
             * EX: url = http://dataservice.accuweather.com/forecasts/{version}/daily/{days}day/{cityCode}
             * parameters = ?apikey={key}
             * uri = http://dataservice.accuweather.com//forecasts/{version}/daily/{days}day/{cityCode}?apikey={key}
             * can use string interpolation to make parameters.
             * -NB
             */
            string apiAddress = baseAddress;
            string parameters = String.Concat("/", uri, cityCode, "?apikey=", key);

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

            }

            return forecastResults;

        }
    }
}
