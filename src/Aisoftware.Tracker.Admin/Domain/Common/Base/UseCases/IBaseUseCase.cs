using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Common.Base.UseCases
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