using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.UseCases.Devices.UseCases;
using Aisoftware.Tracker.UseCases.Geoferences.UseCases;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.UseCases.Base;
using Aisoftware.Tracker.Repositories.Geoferences.Repositories;

namespace Aisoftware.Tracker.UseCases.Geoferences.UseCases;
public class GeoferenceUseCase : BaseUseCase<Geoference>, IGeoferenceUseCase
{
    private readonly IBaseRepository<Geoference> _repository;

    public GeoferenceUseCase(IGeoferenceRepository repository) : base(repository)
    {
        _repository = repository;
    }

}
