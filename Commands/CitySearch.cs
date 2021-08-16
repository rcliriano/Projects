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

namespace Projects.Commands
{

    [Command(Name ="CitySearch", Description ="Search for city using Zip Code", AllowArgumentSeparator = true)]
    

    class CitySearch : CommandBase
    {
        

       
        public CitySearchService service = null;

        private readonly ProjectsContext dbContext;

      

        public CitySearch(IConfiguration config, CitySearchService _service, ProjectsContext _dbContext) : base(config)
        {

            if(_service !=null)
            {
                service = _service;

            }
            if (_dbContext != null)
            {
                dbContext = _dbContext;
            }

        }
        /// <summary>
        /// Runs the AccuWeather API and outputs the result of the citysearchAPI
        /// </summary>
        /// <param name="app"></param>
     
        [Option("-ZC|--ZipCode", Description = "Search for a AccuWeater City Key using Zip Code")]
        [Required]
        public string CityZipCode { get; }
        protected override  void OnExecuteAsync(CommandLineApplication app)
        {
            
            List<RootObject> cityResults =  service.GetCitySearchResponseAsync(CityZipCode);

            
        
                foreach (RootObject cityResult in cityResults)
                {

            
                 using (dbContext) {
                    var cityZip = new CitySearchZip();
                    cityZip.RecordEntryDate = DateTime.UtcNow;
                    cityZip.RecordLastDate = DateTime.UtcNow;
                    cityZip.RecordStatus = "Live";
                    cityZip.Json = JsonConvert.SerializeObject(cityResult);
                    dbContext.CitySearchZip.Add(cityZip);
                    dbContext.SaveChanges();



                }
                    //dbContext.CitySearches.Add(JsonConvert.SerializeObject(cityResult));

                }
            
            
            return;
        }
    }
}
