using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Users.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Users.UseCases
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _repository;

        public UserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            IEnumerable<User> response = await _repository.FindAll();

            return response.OrderBy(user => user.Id);

        }

        public Task<User> FindById(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Save(User request)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Update(User request)
        {
            throw new System.NotImplementedException();
        }

    }
}
