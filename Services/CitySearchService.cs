using System.Collections;
using System.Net.Http;

using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Projects.Helper;
using Projects.Models.CitySearchModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Projects.EFCore.Data;

namespace Projects.Services
{
     class CitySearchService: RestAPIClient

    {

        private readonly IConfiguration _configuration;
        public IConfigurationSection serviceConfigurations;
        private readonly ProjectsContext dbContext;

        public CitySearchService(IConfiguration configuration, ProjectsContext _dbContext) {

            _configuration = configuration;

            if(configuration != null)
            {
               
                serviceConfigurations = _configuration.GetSection("AccuWeatherAPIs");


            }
            if (_dbContext != null)
            {
                dbContext = _dbContext;
            }

        }

        public List <RootObject> GetCitySearchResponseAsync(string zipCode){

            List<RootObject> result = new List<RootObject>();

            //string zipCode = "33545";
            string key = serviceConfigurations.GetValue<string>("key");
            string baseAddress = serviceConfigurations.GetValue<string>("baseAddress");
            string version = serviceConfigurations.GetSection("locations").GetValue<string>("version");
            string uri = string.Concat("locations/", version,serviceConfigurations.GetSection("locations").GetValue<string>("CitySearch"));

            string apiAddress = baseAddress;
            string parameters = String.Concat("/", uri,"apikey=", key, "&q=",zipCode,"\"");

            //string apiAddress = String.Concat("/", uri, "?", "apikey=", key, "&q=Tampa&alias=Florida");
            result = GetRestAPIClient<RootObject>(apiAddress, parameters);
            return result;

            
        }

   
    }

}