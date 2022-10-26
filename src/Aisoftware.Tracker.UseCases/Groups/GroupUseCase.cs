using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.UseCases.Devices.UseCases;
using Aisoftware.Tracker.Repositories.Groups.Repositories;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.UseCases.Base;

namespace Aisoftware.Tracker.UseCases.Groups.UseCases
{
    public class GroupUseCase : BaseUseCase<Group>, IGroupUseCase
    {
        private readonly IBaseRepository<Group> _repository;

        public GroupUseCase(IGroupRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
