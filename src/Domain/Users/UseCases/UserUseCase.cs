using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Users.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;

namespace Aisoftware.Tracker.Admin.Domain.Users.UseCases
{
    public class UserUseCase : BaseUseCase<User>, IUserUseCase
    {
        private readonly IBaseRepository<User> _repository;

        public UserUseCase(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
