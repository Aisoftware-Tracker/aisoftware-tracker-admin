using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;

namespace Aisoftware.Tracker.Admin.Domain.Reports.UseCases
{
    public class ReportSummaryUseCase : IBaseReportUseCase<ReportSummary>
    {
        private readonly IBaseReportRepository<ReportSummary> _repository;

        public ReportSummaryUseCase(IBaseReportRepository<ReportSummary> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReportSummary>> FindAll(IDictionary<string, string> queryParams)
        {
            var response = await _repository.FindAll($"{Endpoints.REPORTS}/{Endpoints.SUMMARY}", queryParams);

            return response;
        }
    }
}
