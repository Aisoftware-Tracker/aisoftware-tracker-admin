using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.Common.Base.UseCases;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.UseCases.Reports.UseCases;
public class ReportRouteUseCase : IBaseReportUseCase<ReportRoute>
{
    private readonly IBaseReportRepository<ReportRoute> _repository;

    public ReportRouteUseCase(IBaseReportRepository<ReportRoute> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReportRoute>> FindAll(IDictionary<string, string> queryParams)
    {
        var response = await _repository.FindAll($"{Endpoints.REPORTS}/{Endpoints.ROUTE}", queryParams);

        return response;
    }
}
