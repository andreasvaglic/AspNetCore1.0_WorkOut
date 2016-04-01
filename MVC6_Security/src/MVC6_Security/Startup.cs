using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MVC6_Security
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            app.UseDeveloperExceptionPage();

            app.UseCookieAuthentication(options =>
            {
                //Select scheme before you have FormsAuth or Windows but just one for project 
                //Now you can have multiple Auth. Scheme in one project working side by side 
                //Because you can have more than one everyone of them need to have a name
                options.AuthenticationScheme = "Cookies";
                options.LoginPath = new PathString("account/login");
                //Before if user try to go somewhere where he can't  
                //the MVC would redirected him to login again that was not so good solution if he is log-in before
                //he is just not allowed to see this page he don't have admin rights or something like that
                options.AccessDeniedPath = new PathString("account/forbidden");

                //Kad zelis definirati dali ce se middleware pokrenuti na pocetku kad request 
                //dodje na stranicu tako da odmah validira cookie i pretvori ga u neki identity
                //i kad odlazis sa strnice pa te redirecta gdje treba
                //Po defaultu to je iskljuceno tako da ako to ne ukljucis stvar ne radi nista
                //Way in
                options.AutomaticAuthenticate = true;
                //Way out
                options.AutomaticChallenge = true;
            });


            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
