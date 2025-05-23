using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories
{
    public interface IBaseReportRepository<T> where T : class
    {     
        Task<IEnumerable<T>> FindAll(string endpoint, IDictionary<string, string> queryParams);
    }

}