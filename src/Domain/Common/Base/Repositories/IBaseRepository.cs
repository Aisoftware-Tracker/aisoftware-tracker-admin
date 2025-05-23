using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories
{
    public interface IBaseRepository<T> where T : class
    {     
        Task<IEnumerable<T>> FindAll(string endpoint);
        Task<T> FindById(int id, string endpoint);
        Task<T> Save(T request, string endpoint);
        Task<T> Update(T request, string endpoint);
        Task<HttpResponseMessage> Delete(int id, string endpoint);
    }

}