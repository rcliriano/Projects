

using System;
using Microsoft.Extensions.Configuration;
using Projects.Helper;
using Projects.Models.CitySearchModel;
using System.Collections.Generic;
using Projects.Models;
using Projects.EFCore.Data;
using System.Linq;
using Projects.EFCore.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Projects.Services
{
    class CitySearchService : RestAPIClient

    {

        private readonly IConfiguration _configuration;
        public IConfigurationSection serviceConfigurations;
        public string ProjectsConnectionString = string.Empty;


        public CitySearchService(IConfiguration configuration)
        {

            _configuration = configuration;


            if (configuration != null)
            {
                serviceConfigurations = _configuration.GetSection("AccuWeatherAPIs");
                ProjectsConnectionString = configuration.GetConnectionString("ProjectsDB");
            }



        }

        public PostDTOCityDetailsModel GetCitySearchResponseAsync(string cityZipCode)
        {

            //Check if CityDetails exist in DB
            PostDTOCityDetailsModel cityDetails = new PostDTOCityDetailsModel();

            cityDetails = GetCityDetails(cityZipCode);


            //Checks if city exist in DB...
            if (cityDetails.CityKey is  null)
            {
                //city doesn't exist then invoke Accuweather API
                List<RootObject> cityResults = new List<RootObject>();

                string key = serviceConfigurations.GetValue<string>("key");
                string baseAddress = serviceConfigurations.GetValue<string>("baseAddress");
                string version = serviceConfigurations.GetSection("locations").GetValue<string>("version");
                string uri = string.Concat("locations/", version, serviceConfigurations.GetSection("locations").GetValue<string>("CitySearch"));

                string apiAddress = baseAddress;
                string parameters = String.Concat("/", uri, "apikey=", key, "&q=", cityZipCode, "\"");

                //string apiAddress = String.Concat("/", uri, "?", "apikey=", key, "&q=Tampa&alias=Florida");
                cityResults = GetRestAPIClient<RootObject>(apiAddress, parameters);

                foreach (RootObject cityResult in cityResults)
                {


                    using (ProjectsContext dbContext = new ProjectsContext(new DbContextOptionsBuilder<ProjectsContext>().UseSqlServer(ProjectsConnectionString).Options))
                    {
                        var cityZip = new CitySearchZip();
                        cityZip.RecordEntryDate = DateTime.UtcNow;
                        cityZip.RecordLastDate = DateTime.UtcNow;
                        cityZip.RecordStatus = "Live";
                        cityZip.Json = JsonConvert.SerializeObject(cityResult);
                        dbContext.CitySearchZips.Add(cityZip);
                        dbContext.SaveChanges();
                        Console.WriteLine(String.Concat("Zip code ", cityZipCode, "found and stored in the database."));
                    }
                }

                //New city entered to DB. Now we get the details of it.
                cityDetails = GetCityDetails(cityZipCode);

            }

            return cityDetails;




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
                return result;

            }


        }

    }
}