using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Aisoftware.Tracker.Admin.Domain.Geoferences.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Geoferences.UseCases
{
    public class GeoferenceUseCase : BaseUseCase<Geoference>, IGeoferenceUseCase
    {
        private readonly IBaseRepository<Geoference> _repository;

        public GeoferenceUseCase(IGeoferenceRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
