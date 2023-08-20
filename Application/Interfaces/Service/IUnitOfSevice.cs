using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IUnitOfSevice
    {
        IAuthService AuthService { get; }
        IFaqService FaqService { get; }
        IMessageAttachmentService MessageAttachmentService { get; }
        IMessageService MessageService { get; }
        ITicketService TicketService { get; }
        
    }
}
