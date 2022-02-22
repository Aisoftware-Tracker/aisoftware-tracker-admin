using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Groups.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;

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
                Document = string.IsNullOrEmpty(content.Document) ? response.Document : content.Document,
                Address = string.IsNullOrEmpty(content.Address) ? response.Address : content.Address,
                City = string.IsNullOrEmpty(content.City) ? response.City : content.City,
                State = string.IsNullOrEmpty(content.State) ? response.State : content.State,
                Country = string.IsNullOrEmpty(content.Country) ? response.Country : content.Country,
                PostalCode = string.IsNullOrEmpty(content.PostalCode) ? response.PostalCode : content.PostalCode,
                Email = string.IsNullOrEmpty(content.Email) ? response.Email : content.Email,
                Phone = string.IsNullOrEmpty(content.Phone) ? response.Phone : content.Phone,
                ValorPlano = string.IsNullOrEmpty(content.ValorPlano) ? response.ValorPlano : content.ValorPlano,
                TipoCobranca = string.IsNullOrEmpty(content.TipoCobranca) ? response.TipoCobranca : content.TipoCobranca,
                DataVencimento = content.DataVencimento ?? response.DataVencimento,
                ObsFinanceiro = string.IsNullOrEmpty(content.ObsFinanceiro) ? response.ObsFinanceiro : content.ObsFinanceiro
            };

            return await _repository.Update(request, Endpoints.GROUPS);

        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id, Endpoints.GROUPS);
        }

    }
}
