using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;

namespace Aisoftware.Tracker.Admin.Domain.Reports.UseCases
{
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
}
