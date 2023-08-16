﻿using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
