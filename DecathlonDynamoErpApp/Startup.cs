using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynamo.Data;
using Dynamo.Model.Common.Authentication;
using Dynamo.Model.Common.Infrastructure;
using Dynamo.Repository.Common;
using Dynamo.Repository.Common.AppSetting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMarkupMin.AspNetCore2;

namespace DecathlonDynamoErpApp
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("DecathlonDynamoErpApp")));

            services.AddTransient<UISettingRepository>();

            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<INotificationGroupRepository, NotificationGroupRepository>();
            services.AddSignalR();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddRoles<ApplicationRole>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/Login/";
                    });

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddWebMarkupMin(options =>
                    {
                        options.AllowMinificationInDevelopmentEnvironment = true;
                        options.AllowCompressionInDevelopmentEnvironment = true;
                    }).AddHtmlMinification(options =>
                        {
                            options.MinificationSettings.RemoveRedundantAttributes = true;
                            options.MinificationSettings.RemoveHttpProtocolFromAttributes = true;
                            options.MinificationSettings.RemoveHttpsProtocolFromAttributes = true;
                        }).AddHttpCompression();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //.AddFeatureFolders()
            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
                app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseWebMarkupMin();
            app.UseSignalR(route =>
            {
                route.MapHub<SignalServer>("/signalServer");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();            // ===== Add this =====
            }

            app.UseHttpsRedirection();    // ===== Add this =====
        }

    }
}
