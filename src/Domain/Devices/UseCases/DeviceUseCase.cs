using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Devices.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Devices.UseCases
{
    public class DeviceUseCase : IDeviceUseCase
    {
        private readonly IBaseRepository<Device> _repository;

        public DeviceUseCase(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Device>> FindAll()
        {
            IEnumerable<Device> response = await _repository.FindAll(Endpoints.DEVICES);

            return response.OrderBy(Device => Device.Id);
        }

        public async Task<Device> FindById(int DeviceId)
        {
            return await _repository.FindById(DeviceId, Endpoints.DEVICES);
        }

        public async Task<Device> Save(Device request)
        {
            return await _repository.Save(request, Endpoints.DEVICES);
        }

        public async Task<Device> Update(Device content)
        {
            Device response = await _repository.FindById(content.Id, Endpoints.DEVICES);

            Device request = new Device
            {
                
            };

            return await _repository.Update(request, Endpoints.DEVICES);

        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id, Endpoints.DEVICES);
        }

    }
}
