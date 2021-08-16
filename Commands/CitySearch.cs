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

namespace Projects.Commands
{

    [Command(Name ="CitySearch", Description = "Pull city details from AccuWeather API")]
    
     class CitySearch: CommandBase
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
       

        protected override  void OnExecuteAsync(CommandLineApplication app)
        {
            List<RootObject> cityResults =  service.GetCitySearchResponseAsync();

            
        
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
