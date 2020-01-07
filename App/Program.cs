using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration(config => config.AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "\\"+"abc.json"))
               .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
