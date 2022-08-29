using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Geoferences.Repositories
{
    public class GeoferenceRepository : BaseRepository<Geoference>, IGeoferenceRepository
    {
        public GeoferenceRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        : base(config, httpContextAccessor)
        {

        }
    }

}
