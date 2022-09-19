using Aisoftware.Tracker.Repositories.Base;
using Aisoftware.Tracker.UseCases.Base.UseCases;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.UseCases.Reports.UseCases;
public class ReportEventUseCase : IBaseReportUseCase<ReportEvent>
{
    private readonly IBaseReportRepository<ReportEvent> _repository;

    public ReportEventUseCase(IBaseReportRepository<ReportEvent> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReportEvent>> FindAll(IDictionary<string, string> queryParams)
    {
        var response = await _repository.FindAll($"{Endpoints.REPORTS}/{Endpoints.EVENTS}", queryParams);

        return response;
    }
}
