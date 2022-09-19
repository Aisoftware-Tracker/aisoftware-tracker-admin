using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Aisoftware.Tracker.Borders.Models;
using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Admin.Domain.Groups.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        : base(config, httpContextAccessor)
        {

        }
    }

}
