using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class MessageAttachmentsRepository : BaseRepository<MessageAttachment>, IMessageAttachmentsRepository
    {
        public MessageAttachmentsRepository(ApplicationDbContext _context) : base(_context) { }

        public async Task<MessageAttachment?> FindById(long id)
        {
            return await _context.MessageAttachments.FindAsync(id);
        }
       
        public async Task AddAsyncWithoutSaveChanges(MessageAttachment messageAttachment)
        {
            await _context.MessageAttachments.AddAsync(messageAttachment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
