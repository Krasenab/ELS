using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ELS.ViewModels.RportsVIewModels;
using ElsModels.SQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service
{
    public class ReportService : IReportService
    {
        private ElsDbContext _dbContext;
        public ReportService(ElsDbContext dbContext)
        {
                this._dbContext = dbContext;
        }
        public void CreateReport(ReportCreateViewModel report)
        {
            Report r = new Report() 
            {
                ReportSerialNumber = report.Title,
                WorkDescription = report.Content,
                TechnicianId = Guid.Parse(report.TechnicianId),
                TicketId = Guid.Parse(report.TicketId),
            };
            _dbContext.Reports.Add(r);
            _dbContext.SaveChanges();
        }

        public async Task<AllReportMainViewModel> FilteredReports(string searchTerm,int page=1)
        {
            
            
            var query = _dbContext.Reports.Include(t=>t.Technician).ThenInclude(tk=>tk.Tickets).AsQueryable();

            if (!String.IsNullOrEmpty(searchTerm))
            {
                string pattern = $"{searchTerm.ToLower().Trim()}%";
                query = query.Where(t => 
                   EF.Functions.Like(t.ReportSerialNumber,pattern) ||
                   EF.Functions.Like(t.Ticket.Title,pattern) ||
                   EF.Functions.Like(t.Technician.FirmId, pattern) ||
                   EF.Functions.Like(t.Ticket.Description,pattern)
                   );
            }

            List<AllReportsVIewModel> allFiltered = await query.Select(r => new AllReportsVIewModel
            {
                EqupmentName = r.Ticket.Equipment.EquipmentName,
                ReportSerialNumber = r.ReportSerialNumber,
                 TechnicianFirmId = r.Technician.FirmId,
                 TicketTitle = r.Ticket.Title,

            }).ToListAsync();
            int totalCount = allFiltered.Count;
            var totalList =  allFiltered.Skip((page - 1) * 3).Take(3).ToList();

            AllReportMainViewModel result = new AllReportMainViewModel()
            {
                
                Reports = allFiltered,
            };
            return result;
        }

        public async Task<List<AllReportsVIewModel>> GetAllReportsAsync()
        {
            List<AllReportsVIewModel> allReports = await _dbContext.Reports.Select(r => new AllReportsVIewModel()
            {
                EqupmentName = r.Ticket.Equipment.EquipmentName,
                ReportSerialNumber = r.ReportSerialNumber,
                TechnicianFirmId = r.Technician.FirmId,
                TicketTitle = r.Ticket.Title

            }).ToListAsync();

            return allReports;
        }

        public async Task<ReportDetailViewModel> GetReportAsync(string reportId)
        {
            ReportDetailViewModel? report = await _dbContext.Reports.Where(id => id.Id.ToString() == reportId)
                .Select(r => new ReportDetailViewModel
                {
                     Id = r.Id.ToString(),
                     ReportSerialNumber = r.ReportSerialNumber,
                     WorkDescription = r.WorkDescription,
                     TechnicianName = r.Technician.AppUser.FirstName,
                     TicketTitle = r.Ticket.Title
                }).FirstOrDefaultAsync();

            return report;
        }

        public async Task<List<ReportDetailViewModel>> GetReportsByTechnicianIdAsync(string technicianId)
        {
            List<ReportDetailViewModel>? reports = await _dbContext.Reports.Where(techId=>techId.TechnicianId.ToString()==technicianId)
               .Select(r => new ReportDetailViewModel
               {
                   Id = r.Id.ToString(),
                   ReportSerialNumber = r.ReportSerialNumber,
                   WorkDescription = r.WorkDescription,
                   TechnicianName = r.Technician.AppUser.FirstName,
                   TicketTitle = r.Ticket.Title
               }).ToListAsync();
            return reports;
        }
    }
}
