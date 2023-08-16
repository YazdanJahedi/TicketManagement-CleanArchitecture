using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public Task SaveAsync();
        public Task<IDbContextTransaction> BeginTransactionAsync();


        // repositories
        public IFAQCategoriesRepository FaqCategoriesRepository { get; }
        public IFAQItemsRepository FaqItemsRepository { get; }
        public IMessageAttachmentsRepository MessageAttachmentsRepository { get; }
        public IMessagesRepository MessagesRepository { get; }
        public ITicketsRepository TicketsRepository { get; }
        public IUsersRepository UsersRepository { get; }
    }
}
