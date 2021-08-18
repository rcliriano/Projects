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


            }


        }

        public List<ForecastRootObject> getForecastAsync(string days) {

            List<ForecastRootObject> result = new List<ForecastRootObject>();

            string key = serviceConfigurations.GetValue<string>("key");
            string baseAddress = serviceConfigurations.GetValue<string>("baseAddress");
            string version = serviceConfigurations.GetSection("forecast").GetValue<string>("version");
            string uri = string.Concat("forecast/", version, serviceConfigurations.GetSection("daily").GetValue<string>(String.Concat(days,"day")));

            string apiAddress = baseAddress;
            string parameters = String.Concat("/", uri,"{citycode}", "?apikey=", key);

            result = GetRestAPIClient<ForecastRootObject>(apiAddress, parameters);

            return result;

        }
       
    }
}
