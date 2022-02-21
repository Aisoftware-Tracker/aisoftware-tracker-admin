using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Reports.Repositories
{
    public class ReportSummaryRepository : BaseReportRepository<ReportSummary>, IReportSummaryRepository
    {
        public ReportSummaryRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        : base(config, httpContextAccessor)
        {

        }
    }

}
