using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Aisoftware.Tracker.Admin.Domain.Services;
using Aisoftware.Tracker.UseCases.Base;
using Aisoftware.Tracker.UseCases.Devices.UseCases;
using Aisoftware.Tracker.UseCases.Drivers.UseCases;
using Aisoftware.Tracker.UseCases.Geoferences.UseCases;
using Aisoftware.Tracker.UseCases.Groups.UseCases;
using Aisoftware.Tracker.UseCases.Maintenances.UseCases;
using Aisoftware.Tracker.UseCases.Positions.UseCases;
using Aisoftware.Tracker.UseCases.Reports.UseCases;
using Aisoftware.Tracker.UseCases.Sessions.UseCases;
using Aisoftware.Tracker.UseCases.Users.UseCases;
using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.Repositories.Common.Configurations;
using Aisoftware.Tracker.Repositories.Devices.Repositories;
using Aisoftware.Tracker.Repositories.Drivers.Repositories;
using Aisoftware.Tracker.Repositories.Geoferences.Repositories;
using Aisoftware.Tracker.Repositories.Groups.Repositories;
using Aisoftware.Tracker.Repositories.Maintenances.Repositories;
using Aisoftware.Tracker.Repositories.Positions.Repositories;
using Aisoftware.Tracker.Repositories.Sessions.Repositories;
using Aisoftware.Tracker.Repositories.Users.Repositories;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Borders.Services;

namespace Aisoftware.Tracker.Admin;

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
        services.AddSession();
        services.AddControllersWithViews();

        services.AddTransient<ITokenService, TokenService>();

        IAppConfiguration appConfig = new AppConfiguration();

        var key = Encoding.ASCII.GetBytes(appConfig.Secret);

        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearer =>
        {
            bearer.RequireHttpsMetadata = false; //TODO verifica na hora que configurar o https
            bearer.SaveToken = true;
            bearer.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddMvc();
        services.AddMemoryCache();
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
        services.AddScoped<IGeoferenceUseCase, GeoferenceUseCase>();
        services.AddScoped<IGeoferenceRepository, GeoferenceRepository>();
        services.AddScoped<IMaintenanceUseCase, MaintenanceUseCase>();
        services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();

        services.AddScoped<ISessionUtil, SessionUtil>();
        services.AddScoped<ILogUtil, LogUtil>();

        services.AddSingleton<IAppConfiguration, AppConfiguration>();
        #endregion

        #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddFile($"../../Logs/{DateTime.Now}.log");

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
        app.UseSession();

        app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

        app.UseStatusCodePages(context =>
        {
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                response.Redirect("/Account/Unauthenticated");
            }

            if (response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                response.Redirect("/Account/Forbidden");
            }

            return System.Threading.Tasks.Task.CompletedTask;
        });

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
