using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Users.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;

namespace Aisoftware.Tracker.Admin.Domain.Users.UseCases
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IBaseRepository<User> _repository;

        public UserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            IEnumerable<User> response = await _repository.FindAll(Endpoints.USERS);

            return response.OrderBy(user => user.Id);
        }

        public async Task<User> FindById(int userId)
        {
            return await _repository.FindById(userId, Endpoints.USERS);
        }

        public async Task<User> Save(User request)
        {
            return await _repository.Save(request, Endpoints.USERS);
        }

        public async Task<User> Update(User user)
        {
            User response = await _repository.FindById(user.Id, Endpoints.USERS);

            User request = new User
            {
                Id = user.Id,
                Name = user.Name ?? response.Name,
                Email = user.Email ?? response.Email,
                Phone = user.Phone ?? response.Phone,
                Readonly = user.Readonly,
                Administrator = user.Administrator,
                Map = user.Map ?? response.Map,
                Latitude = response.Latitude == 0 ? user.Latitude : response.Latitude,
                Longitude = response.Longitude == 0 ? user.Longitude : response.Longitude,
                Zoom = response.Zoom == 0 ? user.Zoom : response.Zoom,
                Password = user.Password ?? response.Password,
                TwelveHourFormat = user.TwelveHourFormat,
                CoordinateFormat = user.CoordinateFormat ?? response.CoordinateFormat,
                Disabled = user.Disabled,
                ExpirationTime = user.ExpirationTime ?? response.ExpirationTime,
                DeviceLimit = response.DeviceLimit == 0 ? user.DeviceLimit : response.DeviceLimit,
                UserLimit = response.UserLimit == 0 ? user.UserLimit : response.UserLimit,
                DeviceReadonly = user.DeviceReadonly,
                LimitCommands = user.LimitCommands,
                PoiLayer = user.PoiLayer ?? response.PoiLayer,
                Token = user.Token ?? response.Token,
                Photo = user.Photo ?? response.Photo,
                Whatsapp = user.Whatsapp ?? response.Whatsapp,
                Telegram = user.Telegram ?? response.Telegram,
                Sms = response.Sms == 0 ? user.Sms : response.Sms,
                Attributes = user.Attributes ?? response.Attributes
            };

            return await _repository.Update(request, Endpoints.USERS);

        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id, Endpoints.USERS);
        }

    }
}
