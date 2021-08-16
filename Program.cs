using Microsoft.Extensions.DependencyInjection;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Projects.Services;
using Projects.Commands;
using Projects.EFCore.Data;
using System;
using System.IO;
using System.Threading.Tasks;


namespace Projects
{

    [Command (Name = "Projects", Description = "Invoke AccuWeather APIs")]
    [Subcommand(
        typeof(CitySearch))
        ]
    public class Program : CommandBase
         
    {
        
        public Program(IConfiguration config) : base(config)
        {
            
        }


        public static async Task Main(string[] args)
        {
            try
            {
                var builder = new HostBuilder().ConfigureAppConfiguration((context, config) =>
                  {
                      config.SetBasePath(Path.Combine(AppContext.BaseDirectory))
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          
                       .AddCommandLine(args);

                      


                  })
                    .ConfigureServices((context, services) =>
                    {
                        services.AddScoped<CitySearchService>(provider => new CitySearchService(context.Configuration));

                        services.AddDbContext<ProjectsContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("ProjectsDB")));




                    });
             
                await builder.RunCommandLineApplicationAsync<Program>(args);
            }
            catch (CommandParsingException cpe)
            {
                Console.WriteLine(cpe.Message);


            }

        }

       
           

            

        protected override void OnExecuteAsync(CommandLineApplication app)
        {
            Console.WriteLine(String.Concat("API URL = ", _config.GetSection("AccuWeatherAPIs")["key"]));
            Console.WriteLine();
        }
    }

       
    }

