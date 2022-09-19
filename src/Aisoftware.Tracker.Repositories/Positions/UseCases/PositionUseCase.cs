using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.Devices.UseCases;
using Aisoftware.Tracker.Positions.Repositories;
using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Positions.UseCases
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
