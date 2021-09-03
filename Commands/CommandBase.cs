using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Projects.Commands
{
    [HelpOption("-h|--help")]
   

    public  abstract class CommandBase
    {
       

        protected IConfiguration _config;
        

        public CommandBase(IConfiguration config)
        {
          
            _config = config;

        }

        protected abstract void OnExecuteAsync(CommandLineApplication app);

        protected void OnValidationError(ValidationResult result, CommandLineApplication app)
        {
            
            Console.WriteLine(String.Concat("API URL = ", _config.GetSection("AccuWeatherAPIs")["key"]));
            Console.WriteLine("Project Forecast --City Miami --State FL --Country US --Days 1");
            Console.WriteLine("Project Forecast --ZipCode --Days 1");
            Console.WriteLine("Project CitySearch --ZipCode 33545");
            app.ShowHelp(true);
            

        }
    }
}
