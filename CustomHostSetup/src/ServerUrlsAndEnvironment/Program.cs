using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace ServerUrlsAndEnvironment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Just uncomment combinations that you want to try
            //Order mater - last added overrides previous
            var configuration = new ConfigurationBuilder()
                //.AddEnvironmentVariables()
                //.AddEnvironmentVariables("MYVALUES_")

                .Build();

            //var environment = configuration["ASPNETCORE_ENVIRONMENT"];


            var host = new WebHostBuilder()
                .UseKestrel()
                //.UseConfiguration(configuration)
                //.UseEnvironment(environment)
                .Configure(appBuilder => appBuilder.Run(async context =>
                    {
                        IHostingEnvironment env = (IHostingEnvironment)appBuilder.ApplicationServices.GetService(typeof(IHostingEnvironment));
                        var serverUrls = appBuilder.ServerFeatures.Get<IServerAddressesFeature>()?.Addresses;
                        await context.Response.WriteAsync($"Environment (ASPNETCORE_ENVIRONMENT): {env.EnvironmentName}\n");
                        await context.Response.WriteAsync($"Listening on address (SERVER.URLS):{string.Join(",", serverUrls)}\n");

                    }))
                .Build();

            host.Run();
        }
    }
}
