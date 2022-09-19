using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Aisoftware.Tracker.Borders.Models;
using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Admin.Domain.Positions.Repositories
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        : base(config, httpContextAccessor)
        {

        }
    }

}
