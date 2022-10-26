
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.UseCases.Base
{
    public interface IBaseReportUseCase<T>
    {
        Task<IEnumerable<T>> FindAll(IDictionary<string, string> queryParams);
    }

}