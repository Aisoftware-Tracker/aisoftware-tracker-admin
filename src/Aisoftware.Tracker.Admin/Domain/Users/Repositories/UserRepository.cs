using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Aisoftware.Tracker.Borders.Models;
using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Admin.Domain.Users.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        : base(config, httpContextAccessor)
        {

        }
    }

}
