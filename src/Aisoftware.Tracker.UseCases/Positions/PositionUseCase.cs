using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.UseCases.Devices.UseCases;
using Aisoftware.Tracker.Repositories.Positions.Repositories;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.UseCases.Base;

namespace Aisoftware.Tracker.UseCases.Positions.UseCases;
public class PositionUseCase : BaseUseCase<Position>, IPositionUseCase
{
    private readonly IBaseRepository<Position> _repository;

    public PositionUseCase(IPositionRepository repository) : base(repository)
    {
        _repository = repository;
    }
    
}
