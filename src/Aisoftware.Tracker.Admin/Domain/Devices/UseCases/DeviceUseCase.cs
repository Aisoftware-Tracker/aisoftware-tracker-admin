using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Admin.Domain.Devices.Repositories;
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
    }
}
