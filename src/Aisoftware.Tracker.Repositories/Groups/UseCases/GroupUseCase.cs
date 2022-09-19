using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.Devices.UseCases;
using Aisoftware.Tracker.Groups.Repositories;
using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Groups.UseCases
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
