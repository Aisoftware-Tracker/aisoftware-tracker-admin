using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;

namespace Aisoftware.Tracker.Admin.Domain.Reports.UseCases
{
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
}
