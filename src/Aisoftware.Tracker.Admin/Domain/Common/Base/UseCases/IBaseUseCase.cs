using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases
{
    public interface IBaseUseCase<T>
    {     
        Task<IEnumerable<T>> FindAll();
        Task<T> FindById(int id);
        Task<T> Save(T request);
        Task<T> Update(T request);
        Task<HttpResponseMessage> Delete(int id);
    }

}