using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Aisoftware.Tracker.Borders.Models;
using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Admin.Domain.Maintenances.Repositories
{
    public class MaintenanceRepository : BaseRepository<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        : base(config, httpContextAccessor)
        {

        }
    }

}
