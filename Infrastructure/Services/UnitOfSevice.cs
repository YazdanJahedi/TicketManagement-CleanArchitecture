using Application.Interfaces.Service;

namespace Infrastructure.Services
{
    public class UnitOfSevice : IUnitOfSevice
    {
        public UnitOfSevice(IAuthService authService, IFaqService faqService, IMessageAttachmentService messageAttachmentService,
                            IMessageService messageService, ITicketService ticketService)
        { 
            AuthService = authService;
            FaqService = faqService;
            MessageAttachmentService = messageAttachmentService;
            TicketService = ticketService;
            MessageService = messageService;
        }
        public IAuthService AuthService { get; set; }

        public IFaqService FaqService { get; set; }

        public IMessageAttachmentService MessageAttachmentService { get; set; } 

        public IMessageService MessageService { get; set; }

        public ITicketService TicketService { get;set; }
    }
}
