using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Devices.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Devices.UseCases
{
    public class DeviceUseCase : BaseUseCase<Device>, IDeviceUseCase
    {
        private readonly IBaseRepository<Device> _repository;

        public DeviceUseCase(IDeviceRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task<Device> Update(Device content)
        {
            Device response = await _repository.FindById(content.Id, Endpoints.DEVICES);

            Device request = new Device
            {
                Id = content.Id,
                Attributes = content.Attributes ?? response.Attributes,
                GroupId = content.GroupId == 0 ? response.GroupId : content.GroupId,
                Name = content.Name ?? response.Name,
                UniqueId = content.UniqueId ?? response.UniqueId,
                Status = content.Status ?? response.Status,
                LastUpdate = content.LastUpdate ?? response.LastUpdate,
                PositionId = content.PositionId == 0 ? response.PositionId : content.PositionId,
                GeofenceIds = content.GeofenceIds ?? response.GeofenceIds,
                Phone = content.Phone ?? response.Phone,
                Model = content.Model ?? response.Model,
                Contact = content.Contact ?? response.Contact,
                Category = content.Category ?? response.Category,
                Disabled = content.Disabled,
                Photo = content.Photo ?? response.Photo,
                Icc = content.Icc ?? response.Icc,
                Vendor = content.Vendor ?? response.Vendor,
                Operator = content.Operator ?? response.Operator,
                Hardware = content.Hardware ?? response.Hardware,
                InfoInstalacao = content.InfoInstalacao ?? response.InfoInstalacao,
                Tech = content.Tech ?? response.Tech,
                LocalInstall = content.LocalInstall ?? response.LocalInstall,
                DateInstall = content.DateInstall ?? response.DateInstall
            };

            return await _repository.Update(request, Endpoints.DEVICES);

        }
    }
}
