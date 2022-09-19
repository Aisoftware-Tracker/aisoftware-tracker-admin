using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.Common.Configurations;
using Aisoftware.Tracker.Borders.Models;
using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Geoferences.Repositories
{
    public class GeoferenceRepository : BaseRepository<Geoference>, IGeoferenceRepository
    {
        public GeoferenceRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        : base(config, httpContextAccessor)
        {

        }
    }

}
