using System.Collections;
using System.Net.Http;

using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Projects.Helper;
using Projects.Models.CitySearchModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projects.Services
{
     class CitySearchService: RestAPIClient

    {

        private readonly IConfiguration _configuration;
        public IConfigurationSection serviceConfigurations;
         

        public CitySearchService(IConfiguration configuration) {

            _configuration = configuration;

            if(configuration != null)
            {
               
                serviceConfigurations = _configuration.GetSection("AccuWeatherAPIs");
               



            }

        }

        public List <RootObject> GetCitySearchResponseAsync(){

            List<RootObject> result = new List<RootObject>();

          
            string key = serviceConfigurations.GetValue<string>("key");
            string baseAddress = serviceConfigurations.GetValue<string>("baseAddress");
            string version = serviceConfigurations.GetSection("locations").GetValue<string>("version");
            string uri = string.Concat("locations/", version,serviceConfigurations.GetSection("locations").GetValue<string>("CitySearch"));

            string apiAddress = baseAddress;
            string parameters = String.Concat("/", uri,"apikey=", key, "&q=33647");
            //string apiAddress = String.Concat("/", uri, "?", "apikey=", key, "&q=Tampa&alias=Florida");
            result = GetRestAPIClient<RootObject>(apiAddress, parameters);
            return result;

            
        }

   
    }

}