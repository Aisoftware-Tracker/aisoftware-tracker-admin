using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Reports.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;

namespace Aisoftware.Tracker.Admin.Domain.Reports.UseCases
{
    public class ReportSummaryUseCase : IReportSummaryUseCase
    {
        private readonly IReportSummaryRepository _repository;

        public ReportSummaryUseCase(IReportSummaryRepository repository)
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
