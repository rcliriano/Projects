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
<<<<<<< HEAD
<<<<<<< HEAD
        }
        
    /// <summary>
    /// Runs the AccuWeather API and outputs the result of the citysearchAPI
    /// </summary>
    /// <param name="app"></param>

<<<<<<< HEAD
<<<<<<< HEAD
        //should add the "ErrorMessage" attribute to the "Required" data annotation. - NB
=======
            if (_dbContext != null)
            {
                dbContext = _dbContext;
            }

=======
            if (_dbContext != null)
            {
                dbContext = _dbContext;
            }

>>>>>>> parent of e2e054d (CitySearchService and ForecastService)
        }
        /// <summary>
        /// Runs the AccuWeather API and outputs the result of the citysearchAPI
        /// </summary>
        /// <param name="app"></param>
     
        [Option("-ZC|--ZipCode", Description = "Search for a AccuWeater City Key using Zip Code")]
<<<<<<< HEAD
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)
=======
    [Option("-ZC|--ZipCode", Description = "Search for a AccuWeater City Key using Zip Code")]
>>>>>>> parent of 64b771f (Added comments/ideas for Ruben)
=======
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)
=======
    [Option("-ZC|--ZipCode", Description = "Search for a AccuWeater City Key using Zip Code")]
>>>>>>> parent of 1b4cd3d (Merge branch 'NoahsCommentBranch' into master)
        [Required]
        public string CityZipCode { get; }
        protected override  void OnExecuteAsync(CommandLineApplication app)
        {

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

<<<<<<< HEAD
            //Invoke AccuWeather API to GET the CityDetails
            PostDTOCityDetailsModel cityResult = service.GetCitySearchResponseAsync(CityZipCode);
=======
        //Invoke AccuWeather API to GET the CityDetails
        PostDTOModel cityResult = service.GetCitySearchResponseAsync(CityZipCode);
        String m = (String.Concat("CityKey", cityResult.CityKey,"\n City:", cityResult.City, "\n State:", cityResult.StateCode, "\n ZipCode:", cityResult.ZipCode, "\n Country:", cityResult.CountryCode));
>>>>>>> parent of 64b771f (Added comments/ideas for Ruben)
=======
            using (dbContext)
            {
                var cityZipCode = dbContext.CitySearchZipViews
                        .Where(b => b.ZipCode == CityZipCode);
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)

                if ((cityZipCode.Count() > 0))
                {
                    cityZipCode.ToString();
                   
                    Console.WriteLine(String.Concat("Zip code ", CityZipCode, " already found."));
                }

<<<<<<< HEAD
<<<<<<< HEAD
            return;
=======
            using (dbContext)
            {
                var cityZipCode = dbContext.CitySearchZipViews
                        .Where(b => b.ZipCode == CityZipCode);
=======
        return;
>>>>>>> parent of 64b771f (Added comments/ideas for Ruben)

                if ((cityZipCode.Count() > 0))
                {
                    cityZipCode.ToString();
                   
                    Console.WriteLine(String.Concat("Zip code ", CityZipCode, " already found."));
                }

                else
                {
>>>>>>> parent of e2e054d (CitySearchService and ForecastService)

                    List<RootObject> cityResults = service.GetCitySearchResponseAsync(CityZipCode);
=======
        //Invoke AccuWeather API to GET the CityDetails
        PostDTOModel cityResult = service.GetCitySearchResponseAsync(CityZipCode);
        String m = (String.Concat("CityKey", cityResult.CityKey,"\n City:", cityResult.City, "\n State:", cityResult.StateCode, "\n ZipCode:", cityResult.ZipCode, "\n Country:", cityResult.CountryCode));

        Console.WriteLine(m);
               
                
            

        return;
>>>>>>> parent of 1b4cd3d (Merge branch 'NoahsCommentBranch' into master)


                    Console.WriteLine(String.Concat("Searching zip code ", CityZipCode));
                    foreach (RootObject cityResult in cityResults)
                    {


                        using (dbContext)
                        {
                            var cityZip = new CitySearchZip();
                            cityZip.RecordEntryDate = DateTime.UtcNow;
                            cityZip.RecordLastDate = DateTime.UtcNow;
                            cityZip.RecordStatus = "Live";
                            cityZip.Json = JsonConvert.SerializeObject(cityResult);
                            dbContext.CitySearchZips.Add(cityZip);
                            dbContext.SaveChanges();
                            Console.WriteLine(String.Concat("Zip code ", CityZipCode, "found and stored in the database."));


=======
                else
                {

                    List<RootObject> cityResults = service.GetCitySearchResponseAsync(CityZipCode);


                    Console.WriteLine(String.Concat("Searching zip code ", CityZipCode));
                    foreach (RootObject cityResult in cityResults)
                    {


                        using (dbContext)
                        {
                            var cityZip = new CitySearchZip();
                            cityZip.RecordEntryDate = DateTime.UtcNow;
                            cityZip.RecordLastDate = DateTime.UtcNow;
                            cityZip.RecordStatus = "Live";
                            cityZip.Json = JsonConvert.SerializeObject(cityResult);
                            dbContext.CitySearchZips.Add(cityZip);
                            dbContext.SaveChanges();
                            Console.WriteLine(String.Concat("Zip code ", CityZipCode, "found and stored in the database."));


>>>>>>> parent of e2e054d (CitySearchService and ForecastService)
                        }
                       

                    }
                }

            }


            return;
        }
    }
}
