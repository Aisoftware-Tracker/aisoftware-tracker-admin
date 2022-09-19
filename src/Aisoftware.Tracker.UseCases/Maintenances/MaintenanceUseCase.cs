using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.UseCases.Devices.UseCases;
using Aisoftware.Tracker.Repositories.Maintenances.Repositories;
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
