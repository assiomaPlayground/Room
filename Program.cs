using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RoomService
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">Run args</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// Create Host builder
        /// </summary>
        /// <param name="args">program args</param>
        /// <returns>Host builder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration( (hostingContext, config) => 
                {
                    config.AddJsonFile("./Utils/ResponseModels/responsemetasettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults( webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
