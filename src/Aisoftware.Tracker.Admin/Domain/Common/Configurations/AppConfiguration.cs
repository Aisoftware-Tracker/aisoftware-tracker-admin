using Microsoft.Extensions.Configuration;
using System.IO;

namespace Aisoftware.Tracker.Common.Configurations
{
    public class AppConfiguration : IAppConfiguration
    {
        private const string APP_SETTINGS = "appsettings.json";
        private readonly string _connectionString;
        private readonly string _baseUrl;
        private readonly string _baseDomain;
        private readonly string _version;
        private readonly string _secret;

        public string ConnectionString { get => _connectionString; }
        public string BaseUrl { get => _baseUrl; }
        public string BaseDomain { get => _baseDomain; }
        public string Version { get => _version; }
        public string Secret { get => _secret; }

        public AppConfiguration()
        {
            var root = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(APP_SETTINGS, false)
                .Build();

            _baseUrl = root.GetSection("Urls").GetSection("BaseUrl").Value;
            _connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            _version = root.GetSection("Version").Value;
            _baseDomain = root.GetSection("Domains").GetSection("BaseDomain").Value;
            _secret = root.GetSection("Token").GetSection("Secret").Value;

        }
    }
}