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

        public IFAQCategoriesRepository FaqCategoriesRepository { get; set; }
        public IFAQItemsRepository FaqItemsRepository { get; set; }
        public IMessageAttachmentsRepository MessageAttachmentsRepository { get; set; }
        public IMessagesRepository MessagesRepository { get; set; }
        public ITicketsRepository TicketsRepository { get; set; }
        public IUsersRepository UsersRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            FaqCategoriesRepository = new FAQCategoriesRepository(context);
            FaqItemsRepository = new FAQItemsRepository(context);
            MessageAttachmentsRepository = new MessageAttachmentsRepository(context);
            MessagesRepository = new MessagesRepository(context);
            TicketsRepository = new TicketsRepository(context);
            UsersRepository = new UsersRepository(context);
        }


        public async Task SaveAsync()
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
