using ELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service.Interfaces
{
    public interface IReportService
    {
        public Task<List<ReportDetailViewModel>> GetReportsByTechnicianIdAsync(string technicianId);
        public Task<ReportDetailViewModel> GetReportAsync(string reportId);
        public void CreateReport(ReportCreateViewModel report);
    }
}
