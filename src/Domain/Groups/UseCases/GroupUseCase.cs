using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Groups.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using System.Net.Http;

namespace Aisoftware.Tracker.Admin.Domain.Groups.UseCases
{
    public class GroupUseCase : IGroupUseCase
    {
        private readonly IBaseRepository<Group> _repository;

        public GroupUseCase(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Group>> FindAll()
        {
            IEnumerable<Group> response = await _repository.FindAll(Endpoints.GROUPS);

            return response.OrderBy(Group => Group.Id);
        }

        public async Task<Group> FindById(int GroupId)
        {
            return await _repository.FindById(GroupId, Endpoints.GROUPS);
        }

        public async Task<Group> Save(Group request)
        {
            return await _repository.Save(request, Endpoints.GROUPS);
        }

        public async Task<Group> Update(Group content)
        {
            Group response = await _repository.FindById(content.Id, Endpoints.GROUPS);

            Group request = new Group
            {
                Id = content.Id,
                Attributes = content.Attributes ?? response.Attributes,
                GroupId = content.GroupId,
                Document = content.Document ?? response.Document,
                Address = content.Address ?? response.Address,
                City = content.City ?? response.City,
                State = content.State ?? response.State,
                Country = content.Country ?? response.Country,
                PostalCode = content.PostalCode ?? response.PostalCode,
                Email = content.Email ?? response.Email,
                Phone = content.Phone ?? response.Phone,
                ValorPlano = content.ValorPlano ?? response.ValorPlano,
                TipoCobranca = content.TipoCobranca ?? response.TipoCobranca,
                DataVencimento = content.DataVencimento ?? response.DataVencimento,
                ObsFinanceiro = content.ObsFinanceiro ?? response.ObsFinanceiro
            };

            return await _repository.Update(request, Endpoints.GROUPS);

        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            return await _repository.Delete(id, Endpoints.GROUPS);
        }

    }
}
