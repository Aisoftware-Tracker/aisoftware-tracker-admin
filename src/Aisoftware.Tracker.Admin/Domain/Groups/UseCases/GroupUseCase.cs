using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Aisoftware.Tracker.Admin.Domain.Groups.Repositories;
using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Admin.Domain.Groups.UseCases
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
