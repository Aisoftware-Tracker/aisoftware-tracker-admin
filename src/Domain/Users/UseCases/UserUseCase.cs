using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Users.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Admin.Domain.Users.UseCases
{
    public class UserUseCase : BaseUseCase<User>, IUserUseCase
    {
        private readonly IBaseRepository<User> _repository;
        private const string MAX_DATE_CENTURY= "2099-12-28T06:00:00.000+0000";

        public UserUseCase(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task<User> Save(User request)
        {
            request.ExpirationTime = MAX_DATE_CENTURY;
            return await _repository.Save(request, Endpoints.USERS);
        }
    }
}
