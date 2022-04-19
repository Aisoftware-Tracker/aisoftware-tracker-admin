using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Users.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using System.Net.Http;

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

        public async Task<User> Update(User content)
        {
            User response = await _repository.FindById(content.Id, Endpoints.USERS);

            User request = new User
            {
                Id = content.Id,
                Attributes = content.Attributes ?? response.Attributes,
                Name = content.Name ?? response.Name,
                Login = content.Login ?? response.Login,
                Email = content.Email ?? response.Email,
                Phone = content.Phone ?? response.Phone,
                Readonly = content.Readonly,
                Administrator = content.Administrator,
                Map = content.Map ?? response.Map,
                Latitude = response.Latitude == 0 ? content.Latitude : response.Latitude,
                Longitude = response.Longitude == 0 ? content.Longitude : response.Longitude,
                Zoom = response.Zoom == 0 ? content.Zoom : response.Zoom,
                TwelveHourFormat = content.TwelveHourFormat,
                CoordinateFormat = content.CoordinateFormat ?? response.CoordinateFormat,
                Disabled = content.Disabled,
                ExpirationTime = content.ExpirationTime ?? response.ExpirationTime,
                DeviceLimit = response.DeviceLimit == 0 ? content.DeviceLimit : response.DeviceLimit,
                UserLimit = response.UserLimit == 0 ? content.UserLimit : response.UserLimit,
                DeviceReadonly = content.DeviceReadonly,
                LimitCommands = content.LimitCommands,
                PoiLayer = content.PoiLayer ?? response.PoiLayer,
                Token = content.Token ?? response.Token,
                Photo = content.Photo ?? response.Photo,
                Whatsapp = content.Whatsapp ?? response.Whatsapp,
                Telegram = content.Telegram ?? response.Telegram,
                Sms = response.Sms == 0 ? content.Sms : response.Sms,
                Password = content.Password ?? response.Password
            };

            return await _repository.Update(request, Endpoints.USERS);

        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            return await _repository.Delete(id, Endpoints.USERS);
        }

    }
}
