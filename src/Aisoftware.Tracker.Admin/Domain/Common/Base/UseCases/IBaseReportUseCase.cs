
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases
{
    public interface IBaseReportUseCase<T>
    {
        Task<IEnumerable<T>> FindAll(IDictionary<string, string> queryParams);
    }

}