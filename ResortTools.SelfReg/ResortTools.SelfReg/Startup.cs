using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResortTools.SelfReg.Interfaces;
using ResortTools.SelfReg.Services;
using UnityAPI.ClientLibrary;

namespace ResortTools.SelfReg
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Add a RegistrationConfig object to the services and hydrate it with the
            //data from teh appsettings.json file
            services.AddOptions();
            var SelfRegistrationConfig = new SelfRegistrationConfig();
            Configuration.Bind("SelfRegistrationConfig", SelfRegistrationConfig);
            services.AddSingleton(SelfRegistrationConfig);

            services.AddTransient<IContactService, ContactService>();

            //TODO: Replace this with referenceds to real services 
            services.AddTransient<IWaiverService, WaiverServiceMock>();
            services.AddTransient<IRegistrationService, RegistrationServiceMock>();

            services.Configure<UnityClientSettings>(Configuration.GetSection("UnityClient"));
            services.AddUnityClientLibrary();


            // Bind options using a sub-section of the appsettings.json file.
            //services.Configure<RegistrationConfig>(Configuration.GetSection("RegistrationConfig"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Read the configuration from the appsettings.json
            var builder = new ConfigurationBuilder()
                   .SetBasePath(env.ContentRootPath)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                   .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
