using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Data;

using DataTables.AspNet.AspNetCore;
using System;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using System.IO;
namespace ClinicManagementFrontEnd
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
            
            

           

            var options = new DataTables.AspNet.AspNetCore.Options()
  .EnableRequestAdditionalParameters()
  .EnableResponseAdditionalParameters();

            var binder = new DataTables.AspNet.AspNetCore.ModelBinder();

            binder.ParseAdditionalParameters = Parser;

           
           
            
            services.AddControllersWithViews();
            //services.AddRazorPages()
            // .AddRazorRuntimeCompilation();


            services.AddSingleton<ClinicManagementFrontEnd.Lookup.iLookup, ClinicManagementFrontEnd.Lookup.Lookup>();
            services.AddServerSideBlazor();
            services.RegisterDataTables(options, binder);
            //services.AddExpressiveAnnotations();

        }
        private IDictionary<string, object> Parser(ModelBindingContext modelBindingContext)
        {
            var additionalParameterKey = modelBindingContext.ValueProvider.GetValue("AdditionalParameters");
            var appJson = additionalParameterKey != null ? additionalParameterKey.FirstValue ?? "{}" : "{}";
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, object>>(appJson);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    //app.UseMigrationsEndPoint();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //var path = Directory.GetCurrentDirectory();
            //loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            app.UseExceptionHandler("/Home/Error");
           // app.UseStatusCodePagesWithReExecute("/error/{0}");
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
           


            app.UseEndpoints(endpoints =>
            {
                //app.UseEndpoints(endpoints =>
                //{
                //    endpoints.MapControllerRoute(
                //      name: "areas",
                //      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                //    );
                //});
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
                //endpoints.MapBlazorHub();
            });
        }
    }
}
