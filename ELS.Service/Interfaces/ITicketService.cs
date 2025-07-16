using Els.ViewModels.Enums;
using ELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service.Interfaces
{
    public interface ITicketService
    {
        public Task CreateTicketAsync(CreateTicketViewModel inputModel);

        public List<TicketPriority> GetPriorities();

    }
}
