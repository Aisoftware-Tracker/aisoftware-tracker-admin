using Microsoft.Extensions.Configuration;
using System.IO;

namespace Aisoftware.Tracker.Worker.DAO.Common.Configurations;

public class AppConfiguration : IAppConfiguration
{
    private const string APP_SETTINGS = "appsettings.json";
    private readonly string _connectionString;
    private readonly string _version;
    private readonly string _secret;

    public string ConnectionString { get => _connectionString; }
    public string Version { get => _version; }
    public string Secret { get => _secret; }

    public AppConfiguration()
    {
        var root = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(APP_SETTINGS, false)
            .Build();

        _connectionString = root.GetSection("ConnectionStrings").GetSection("ConnectionString").Value;
        _version = root.GetSection("Version").Value;
        _secret = root.GetSection("Token").GetSection("Secret").Value;

    }
}
