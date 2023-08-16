using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public void SaveAsync();
        public Task<IDbContextTransaction> BeginTransactionAsync();


        // repositories
        public IFAQCategoriesRepository _faqCategoriesRepository { get; }
        public IFAQItemsRepository _faqItemsRepository { get; }
        public IMessageAttachmentsRepository _messageAttachmentsRepository { get; }
        public IMessagesRepository _messagesRepository { get; }
        public ITicketsRepository _ticketsRepository { get; }
        public IUsersRepository _usersRepository { get; }
    }
}
