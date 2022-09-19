using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Aisoftware.Tracker.Admin.Domain.Positions.Repositories;
using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Admin.Domain.Positions.UseCases
{
    public class PositionUseCase : BaseUseCase<Position>, IPositionUseCase
    {
        private readonly IBaseRepository<Position> _repository;

        public PositionUseCase(IPositionRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
