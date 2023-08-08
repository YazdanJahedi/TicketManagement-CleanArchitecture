﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IMessageAttachmentsRepository : IBaseRepository<MessageAttachment>
    {
        public Task<MessageAttachment?> FindById(long id);
        public Task AddAsyncWithoutSaveChanges(MessageAttachment messageAttachment);
        public Task SaveChangesAsync();
    }
}
