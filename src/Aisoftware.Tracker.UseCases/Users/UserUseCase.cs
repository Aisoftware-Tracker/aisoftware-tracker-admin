using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.Devices.UseCases;
using Aisoftware.Tracker.Users.Repositories;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Models;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Users.UseCases
{
    public class UserUseCase : BaseUseCase<User>, IUserUseCase
    {
        private readonly IBaseRepository<User> _repository;
        private const string MAX_DATE_CENTURY = "2099-12-28T06:00:00.000+0000";

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
