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
using Projects.Models;

namespace Projects.Commands
{

    [Command(Name = "Forecast", Description = "Get city forecast", AllowArgumentSeparator = true)]

    [HelpOption("-h|--help")]

    class Forecast : CommandBase
    {



        public ForecastService forecastService = null;
        public CitySearchService citySearchService = null;
        private readonly ProjectsContext dbContext;
       
        public Forecast(IConfiguration config, ForecastService _forecastService, CitySearchService _citySearchService, ProjectsContext _dbContext) : base(config)
        {

            if (_forecastService != null)
            {
                forecastService = _forecastService;

            }
            if (_dbContext != null)
            {
                dbContext = _dbContext;
            }
            if(_citySearchService != null)
            {

                citySearchService = _citySearchService;

            }


        }
        /// <summary>
        /// Runs the AccuWeather API and outputs the result of the ForecastAPI
        /// </summary>
        /// <param name="app"></param>

    
        [Required]
        [Option("-ZC|--ZipCode", Description = "Get Forecast Details for the entered zipcode")]
        public string cityZipCode { get; }

        [Required]
        [Option("-D|--Days", Description = "Get Forecast in days")]
        [AllowedValues("1","5")]
        public string days { get; }

        protected override  void OnExecuteAsync(CommandLineApplication app)
        {

            //Gets from CitySearchService the CityDetails.
            PostDTOModel cityDetails = citySearchService.GetCitySearchResponseAsync(cityZipCode);

            ForecastRootObject forecast = forecastService.GetForecastAsync(cityDetails.CityKey, days);

            


            return;
        }
    }
}
