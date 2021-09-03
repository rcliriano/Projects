

using System;
using Microsoft.Extensions.Configuration;
using Projects.Helper;
using Projects.Models.CitySearchModel;
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

<<<<<<< HEAD
<<<<<<< HEAD
        /// <summary>
        /// if you do "///" above a function then it will auto generate the documentation structure and will show up when you hover over the method when called from another class.
        /// Hover over GetForecastAsync in line 74 in Forecast.cs to see.
        /// -NB
        /// </summary>
        /// <param name="cityZipCode">The Zipcode of the city being requested</param>
        /// <returns>Accuweathers API details on the city requested</returns>
        public PostDTOCityDetailsModel GetCitySearchResponseAsync(string cityZipCode)
=======
        public PostDTOModel GetCitySearchResponseAsync(string cityZipCode)
>>>>>>> parent of 64b771f (Added comments/ideas for Ruben)
        {

            //Check if CityDetails exist in DB
            PostDTOCityDetailsModel cityDetails = new PostDTOCityDetailsModel();
=======
        public List <RootObject> GetCitySearchResponseAsync(string zipCode){
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)

            
            List<RootObject> result = new List<RootObject>();

           

                string key = serviceConfigurations.GetValue<string>("key");
                string baseAddress = serviceConfigurations.GetValue<string>("baseAddress");
                string version = serviceConfigurations.GetSection("locations").GetValue<string>("version");
                string uri = string.Concat("locations/", version, serviceConfigurations.GetSection("locations").GetValue<string>("CitySearch"));

                string apiAddress = baseAddress;
<<<<<<< HEAD
<<<<<<< HEAD

                /*
                 * can use string interpolation to make parameters
                 * parameters should be anything after the ?. 
                 * EX: apiAddress = http://dataservice.accuweather.com/cities/US/search
                 * parameters = ?apikey=<key>&q=<zipcode> 
                 * -NB
                 */
=======
>>>>>>> parent of 64b771f (Added comments/ideas for Ruben)
                string parameters = String.Concat("/", uri, "apikey=", key, "&q=", cityZipCode, "\"");
=======
                string parameters = String.Concat("/", uri, "apikey=", key, "&q=", zipCode, "\"");
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)

                //string apiAddress = String.Concat("/", uri, "?", "apikey=", key, "&q=Tampa&alias=Florida");
                result = GetRestAPIClient<RootObject>(apiAddress, parameters);


<<<<<<< HEAD

        }
        public PostDTOCityDetailsModel GetCityDetails(string CityZipCode)
        {

            PostDTOCityDetailsModel result = new PostDTOCityDetailsModel();

            using (ProjectsContext dbContext = new ProjectsContext(new DbContextOptionsBuilder<ProjectsContext>().UseSqlServer(ProjectsConnectionString).Options))
            {
                {
                    var cityDetails = dbContext.CitySearchZipViews
                            .Where(b => b.ZipCode == CityZipCode)
                            .Select(p => new PostDTOCityDetailsModel
                            {
                                CityKey = p.CityKey
                                ,
                                City = p.City
                                ,
                                StateCode = p.StateCode
                                ,
                                CountryCode = p.CountryCode
                                ,
                                ZipCode = p.ZipCode
                            });

                    foreach (var cityDetail in cityDetails)
                    {
                        if ((cityDetail.CityKey.Count() > 0))
                        {

                            result = cityDetail;

                        }


                    }


                }
=======
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)
                return result;
            
            

            
        }

   
    }

}