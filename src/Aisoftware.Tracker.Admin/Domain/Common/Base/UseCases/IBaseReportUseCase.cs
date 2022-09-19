
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Common.Base.UseCases
{
    public interface IBaseReportUseCase<T>
    {
        Task<IEnumerable<T>> FindAll(IDictionary<string, string> queryParams);
    }

}