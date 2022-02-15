using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories
{
    public interface IBaseRepository<T> where T : class
    {     
        Task<IEnumerable<T>> FindAll(string endpoint);
        Task<T> FindById(int id, string endpoint);
        Task<T> Save(T request, string endpoint);
        Task<T> Update(int id, T request, string endpoint);
        Task Delete(int id, string endpoint);
    }

}