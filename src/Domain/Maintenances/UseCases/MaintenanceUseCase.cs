using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Maintenances.Repositories;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;

namespace Aisoftware.Tracker.Admin.Domain.Maintenances.UseCases
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
