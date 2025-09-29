using ELS.ViewModels;
using ELS.ViewModels.RportsVIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service.Interfaces
{
    public interface IReportService
    {
        public Task<bool> HasReportForTicketAsync(string ticketId);
        public Task<AllReportMainViewModel> FilteredReports(string searchTerm,int page);
        public Task<List<AllReportsVIewModel>> GetAllReportsAsync();
        public Task<List<ReportDetailViewModel>> GetReportsByTechnicianIdAsync(string technicianId);
        public Task<ReportDetailViewModel> GetReportAsync(string reportId);
        public void CreateReport(ReportCreateViewModel report);
    }
}
