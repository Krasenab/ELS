using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;
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
    }
}
