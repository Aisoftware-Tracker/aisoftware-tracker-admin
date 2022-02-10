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

        public async Task<User> FindById(int userId)
        {
            return await _repository.FindById(userId);
        }

        public async Task<User> Save(User request)
        {
            return await _repository.Save(request);
        }

        public async Task<User> Update(User request)
        {
            User user = await _repository.FindById(request.Id);

            User content = new User
            {
                Id = request.Id,
                Name = user.Name ?? request.Name,
                Email = user.Email ?? request.Email,
                Phone = user.Phone ?? request.Phone,
                Readonly = request.Readonly,
                Administrator = request.Administrator,
                Map = user.Map ?? request.Map,
                Latitude = user.Latitude == 0 ? request.Latitude : user.Latitude,
                Longitude = user.Longitude == 0 ? request.Longitude : user.Longitude,
                Zoom = user.Zoom == 0 ? request.Zoom : user.Zoom,
                Password = user.Password ?? request.Password,
                TwelveHourFormat = request.TwelveHourFormat,
                CoordinateFormat = user.CoordinateFormat ?? request.CoordinateFormat,
                Disabled = request.Disabled,
                ExpirationTime = user.ExpirationTime ?? request.ExpirationTime,
                DeviceLimit = user.DeviceLimit == 0 ? request.DeviceLimit : user.DeviceLimit,
                UserLimit = user.UserLimit == 0 ? request.UserLimit : user.UserLimit,
                DeviceReadonly = request.DeviceReadonly,
                LimitCommands = request.LimitCommands,
                PoiLayer = user.PoiLayer ?? request.PoiLayer,
                Token = user.Token ?? request.Token,
                Photo = user.Photo ?? request.Photo,
                Whatsapp = user.Whatsapp ?? request.Whatsapp,
                Telegram = user.Telegram ?? request.Telegram,
                Sms = user.Sms == 0 ? request.Sms : user.Sms,
                Attributes = user.Attributes ?? request.Attributes
            };

            return await _repository.Update(content);
        }

    }
}
