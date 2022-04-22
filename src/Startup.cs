using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Sessions.UseCases;
using Aisoftware.Tracker.Admin.Domain.Sessions.Repositories;
using Aisoftware.Tracker.Admin.Domain.Users.UseCases;
using Aisoftware.Tracker.Admin.Domain.Users.Repositories;
using Aisoftware.Tracker.Admin.Domain.Drivers.UseCases;
using Aisoftware.Tracker.Admin.Domain.Drivers.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Aisoftware.Tracker.Admin.Domain.Devices.Repositories;
using Aisoftware.Tracker.Admin.Domain.Groups.UseCases;
using Aisoftware.Tracker.Admin.Domain.Groups.Repositories;
using Aisoftware.Tracker.Admin.Domain.Reports.UseCases;
using Microsoft.Extensions.Logging;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;
using Aisoftware.Tracker.Admin.Domain.Positions.UseCases;
using Aisoftware.Tracker.Admin.Domain.Positions.Repositories;
using Aisoftware.Tracker.Admin.Common.Util;

namespace Aisoftware.Tracker.Admin
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
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Dependency Injection

            #region Use Cases && Repositories
            services.AddScoped<ISessionUseCase, SessionUseCase>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IUserUseCase, UserUseCase>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDriverUseCase, DriverUseCase>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IDeviceUseCase, DeviceUseCase>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IGroupUseCase, GroupUseCase>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IPositionUseCase, PositionUseCase>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IBaseReportUseCase<ReportSummary>, ReportSummaryUseCase>();
            services.AddScoped<IBaseReportRepository<ReportSummary>, BaseReportRepository<ReportSummary>>();
            services.AddScoped<IBaseReportUseCase<ReportRoute>, ReportRouteUseCase>();
            services.AddScoped<IBaseReportRepository<ReportRoute>, BaseReportRepository<ReportRoute>>();
            services.AddScoped<IBaseReportUseCase<ReportEvent>, ReportEventUseCase>();
            services.AddScoped<IBaseReportRepository<ReportEvent>, BaseReportRepository<ReportEvent>>();

            services.AddScoped<ISessionUtil, SessionUtil>();
            services.AddScoped<ILogUtil, LogUtil>();

            services.AddScoped<IAppConfiguration, AppConfiguration>();
            #endregion

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile($"../Logs/{DateTime.Now}.log");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
