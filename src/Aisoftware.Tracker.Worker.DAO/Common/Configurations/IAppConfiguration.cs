namespace Aisoftware.Tracker.Worker.DAO.Common.Configurations;

public interface IAppConfiguration
{
    string ConnectionString { get; }
    string Secret { get; }
}
