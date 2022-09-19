namespace Aisoftware.Tracker.Common.Configurations
{
    public interface IAppConfiguration
    {
        string ConnectionString { get; }
        string BaseUrl { get; }
        string BaseDomain { get; }
        string Secret { get; }
    }
}