using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace hub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => {
                    services.AddLogging();
                    services.AddCors(options => options.AddPolicy("CorsPolicy", 
                        builder => 
                        {
                            builder
                                .AllowCredentials()
                                .AllowAnyMethod()
                                .WithHeaders("X-Requested-With")    
                                .WithOrigins("https://localhost:5001");
                        }));
                    services.AddMvc();
                    services.AddSignalR();
                    services.AddHostedService<Worker>();
                    services.AddHealthChecks();
                });
    }
}
