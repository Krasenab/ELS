using ELS.Data;
using ELS.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service
{
    public class TicketService : ITicketService
    {
        private ElsDbContext _dbContext;
        public TicketService(ElsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task CreateTicket()
        {
            throw new NotImplementedException();
        }
    }
}
