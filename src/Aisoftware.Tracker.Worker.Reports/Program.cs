using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Worker.Reports;
using Aisoftware.Tracker.Worker.DAO.Common.Base;
using Aisoftware.Tracker.Worker.DAO.Users;
using Aisoftware.Tracker.Worker.DAO.Common.Configurations;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IBaseDAO<User>, UserDAO>();
        services.AddSingleton<IAppConfiguration, AppConfiguration>();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
