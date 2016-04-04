using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authentication.Google;

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



            app.UseCookieAuthentication(options =>
            {
                //Select scheme before you have FormsAuth or Windows but just one for project 
                //Now you can have multiple Auth. Scheme in one project working side by side 
                //Because you can have more than one everyone of them need to have a name
                options.AuthenticationScheme = "Cookies";
                options.LoginPath = new PathString("/account/login");
                //Before if user try to go somewhere where he can't  
                //the MVC would redirected him to login again that was not so good solution if he is log-in before
                //he is just not allowed to see this page he don't have admin rights or something like that
                options.AccessDeniedPath = new PathString("/account/forbidden");
                //If you don't set those properties to true and they are by default set to false 
                //your authentication will not work
                //Your are defining when the middle-ware need to start working 
                //Way in
                options.AutomaticAuthenticate = true;
                //Way out
                options.AutomaticChallenge = true;
            });

            //Other way how you can define auth middle-ware properties
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationScheme = "Cookies",
            //    LoginPath = "/account/login",
            //    AccessDeniedPath = "/account/forbidden",
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});


            //Google auth.
            //https://console.developers.google.com/apis/credentials?project=mvcsecutiryworkout
            //You need to define Authorized redirect URIs on Google page and enable Google+ API
            //default redirect URI that need to be define is http://localhost:52506/signin-google
            //app.UseGoogleAuthentication(options =>
            //{
            //    options.AuthenticationScheme = "Google";
            //    options.SignInScheme = "Cookies";
            //    options.ClientId = "781644176920-4smb90rptgavjf28qrvrcth0imrgel8s.apps.googleusercontent.com";
            //    options.ClientSecret = "mMb4kX4MXthl7eVXjWxa0dKi";
            //}); 
            app.UseGoogleAuthentication(new GoogleOptions()
            {
                ClientId = "781644176920-4smb90rptgavjf28qrvrcth0imrgel8s.apps.googleusercontent.com",
                ClientSecret = "mMb4kX4MXthl7eVXjWxa0dKi",
                AuthenticationScheme = "Google",
                //With SignInScheme property we are defining what middle-ware will continue to process data when
                //is back from external provider
                SignInScheme = "Cookies"
                //If you define redirect page on this way the process will not work because you will be always
                //redirected to Google again
                //CallbackPath = "/Home/Secure/"
            });


            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
