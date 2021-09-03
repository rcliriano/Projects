using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Projects.Services;
using Projects.Models.CitySearchModel;
using System.Collections.Generic;
using Projects.EFCore.Data;
using Projects.EFCore.Models;
using Newtonsoft.Json;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Projects.Commands
{

    [Command(Name = "Forecast", Description = "Get city forecast", AllowArgumentSeparator = true)]

    [HelpOption("-h|--help")]

    class Forecast : CommandBase
    {



        public ForecastService service = null;
      
        private readonly ProjectsContext dbContext;



        public Forecast(IConfiguration config, ForecastService _service, ProjectsContext _dbContext) : base(config)
        {

            if (_service != null)
            {
                service = _service;

            }
            if (_dbContext != null)
            {
                dbContext = _dbContext;
            }

        }
        /// <summary>
        /// Runs the AccuWeather API and outputs the result of the ForecastAPI
        /// </summary>
        /// <param name="app"></param>

<<<<<<< HEAD
        //should add ErrorMessage Attribute to the "Required" data annotation 
        [Required]
        [Option("-ZC|--ZipCode", Description = "Get Forecast Details for the entered zipcode")]
        public string cityZipCode { get; }

        //should add ErrorMessage Attribute to the "Required" data annotation -NB
        [Required]
=======
        [Option("-C|--City", Description = "City to get Forecast in days")]
        public string city {get;}

        [Option("-S|--State", Description = "State to get Forecast in days")]
        public string state { get; }

        [Option("-C|--Country", Description = "State to get Forecast in days")]
        public string country { get; }

        [Option("-ZC|--ZipCode", Description = "State to get Forecast in days")]
        public string zipCode { get; }

>>>>>>> parent of e2e054d (CitySearchService and ForecastService)
        [Option("-D|--Days", Description = "Get Forecast in days")]
        [AllowedValues("1","5")]
        [Required]
        public string days { get; }

        protected override  void OnExecuteAsync(CommandLineApplication app)
        {
            using (dbContext)
            {

                if (zipCode.Count() > 0)
                {
                    var cityKey = dbContext.CitySearchZipViews
                    .Where(b => b.ZipCode == zipCode);
                }
                else if (city.Count() > 0 && state.Count() > 0 && country.Count() > 0)
                {
                    var cityKey = dbContext.CitySearchZipViews
                        .Where(b => b.City == city && b.StateCode == state && b.CountryCode == country);
                }


            }


<<<<<<< HEAD
            //Gets from CitySearchService the CityDetails.
            PostDTOCityDetailsModel cityDetails = citySearchService.GetCitySearchResponseAsync(cityZipCode);
=======
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)




            return;
        }
    }
}
