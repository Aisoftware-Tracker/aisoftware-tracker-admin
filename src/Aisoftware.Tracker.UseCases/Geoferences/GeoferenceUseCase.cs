using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.UseCases.Devices.UseCases;
using Aisoftware.Tracker.Repositories.Geoferences.Repositories;
using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.UseCases.Geoferences.UseCases;
public class GeoferenceUseCase : BaseUseCase<Geoference>, IGeoferenceUseCase
{
    private readonly IBaseRepository<Geoference> _repository;

    public GeoferenceUseCase(IGeoferenceRepository repository) : base(repository)
    {
        _repository = repository;
    }

}
