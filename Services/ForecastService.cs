using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Projects.EFCore.Data;
using Projects.Models;
using Projects.Helper;

namespace Projects.Services
{
     class ForecastService : RestAPIClient
    {
        private readonly IConfiguration _configuration;
        public IConfigurationSection serviceConfigurations;
        private readonly ProjectsContext dbContext = new ProjectsContext();

        public ForecastService(IConfiguration configuration)
        {

            _configuration = configuration;

            if (configuration != null)
            {

                serviceConfigurations = _configuration.GetSection("AccuWeatherAPIs");
<<<<<<< HEAD
<<<<<<< HEAD
                ProjectsConnectionString = configuration.GetConnectionString("ProjectsDB");
=======
=======
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)


>>>>>>> parent of e2e054d (CitySearchService and ForecastService)
            }


        }

        public List<ForecastRootObject> getForecastAsync(string days) {

            List<ForecastRootObject> result = new List<ForecastRootObject>();

            string key = serviceConfigurations.GetValue<string>("key");
            string baseAddress = serviceConfigurations.GetValue<string>("baseAddress");
            string version = serviceConfigurations.GetSection("forecast").GetValue<string>("version");
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            
            /*
             * uri is typically refered to as the whole request. so baseaddress/apiaddress?parameters
             * instead of using string.Concat you can store the api address as /forecasts/{0}/daily/{1}day/{2} and use string.format to replace the {x} values.
             * -NB
             */
=======
>>>>>>> parent of 64b771f (Added comments/ideas for Ruben)
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
=======
            string uri = string.Concat("forecast/", version, serviceConfigurations.GetSection("daily").GetValue<string>(String.Concat(days,"day")));

            string apiAddress = baseAddress;
            string parameters = String.Concat("/", uri,"{citycode}", "?apikey=", key);

            result = GetRestAPIClient<ForecastRootObject>(apiAddress, parameters);

            return result;
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)
=======
            string uri = string.Concat("forecast/", version, serviceConfigurations.GetSection("daily").GetValue<string>(String.Concat(days,"day")));

            string apiAddress = baseAddress;
            string parameters = String.Concat("/", uri,"{citycode}", "?apikey=", key);

            result = GetRestAPIClient<ForecastRootObject>(apiAddress, parameters);

            return result;
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)

        }
       
    }
}
