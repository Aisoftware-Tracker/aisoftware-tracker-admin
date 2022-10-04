using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.Repositories.Devices.Repositories;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.UseCases.Base;

namespace Aisoftware.Tracker.UseCases.Devices.UseCases;
public class DeviceUseCase : BaseUseCase<Device>, IDeviceUseCase
{
    private readonly IBaseRepository<Device> _repository;

    public DeviceUseCase(IDeviceRepository repository) : base(repository)
    {
        _repository = repository;
    }
}
