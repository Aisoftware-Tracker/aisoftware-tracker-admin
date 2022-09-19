using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Repositories.Base;
public interface IBaseReportRepository<T> where T : class
{
    Task<IEnumerable<T>> FindAll(string endpoint, IDictionary<string, string> queryParams);
}
