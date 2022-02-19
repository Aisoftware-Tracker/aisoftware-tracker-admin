using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;

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
