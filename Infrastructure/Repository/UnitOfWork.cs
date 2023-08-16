using Application.Interfaces.Repository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IFAQCategoriesRepository _faqCategoriesRepository { get; set; }
        public IFAQItemsRepository _faqItemsRepository { get; set; }
        public IMessageAttachmentsRepository _messageAttachmentsRepository { get; set; }
        public IMessagesRepository _messagesRepository { get; set; }
        public ITicketsRepository _ticketsRepository { get; set; }
        public IUsersRepository _usersRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _faqCategoriesRepository = new FAQCategoriesRepository(context);
            _faqItemsRepository = new FAQItemsRepository(context);
            _messageAttachmentsRepository = new MessageAttachmentsRepository(context);
            _messagesRepository = new MessagesRepository(context);
            _ticketsRepository = new TicketsRepository(context);
            _usersRepository = new UsersRepository(context);
        }


        public async void SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
