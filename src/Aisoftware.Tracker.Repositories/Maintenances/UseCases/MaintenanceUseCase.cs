using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.Devices.UseCases;
using Aisoftware.Tracker.Maintenances.Repositories;
using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Maintenances.UseCases
{
    public class MaintenanceUseCase : BaseUseCase<Maintenance>, IMaintenanceUseCase
    {
        private readonly IBaseRepository<Maintenance> _repository;

        public MaintenanceUseCase(IMaintenanceRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
