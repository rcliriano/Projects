using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Projects.Services;
using Projects.Models.CitySearchModel;
using System.Collections.Generic;
using Projects.EFCore.Data;
using Projects.EFCore.Models;
using Newtonsoft.Json;
using Projects.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Projects.Commands
{

    [Command(Name ="CitySearch", Description ="Search for city using Zip Code", AllowArgumentSeparator = true)]
    

    class CitySearch : CommandBase
    {
        
        public CitySearchService service = null;

        public CitySearch(IConfiguration config, CitySearchService _service) : base(config)
        {

            if(_service !=null)
            {
                service = _service;

            }
        }
        
    /// <summary>
    /// Runs the AccuWeather API and outputs the result of the citysearchAPI
    /// </summary>
    /// <param name="app"></param>

    [Option("-ZC|--ZipCode", Description = "Search for a AccuWeater City Key using Zip Code")]
        [Required]
        public string CityZipCode { get; }
        protected override void OnExecuteAsync(CommandLineApplication app)
        {
           

        //Invoke AccuWeather API to GET the CityDetails
        PostDTOCityDetailsModel cityResult = service.GetCitySearchResponseAsync(CityZipCode);
        String m = (String.Concat("CityKey", cityResult.CityKey,"\n City:", cityResult.City, "\n State:", cityResult.StateCode, "\n ZipCode:", cityResult.ZipCode, "\n Country:", cityResult.CountryCode));

        Console.WriteLine(m);
               
                
            

        return;

        }

        


    }



        
}
