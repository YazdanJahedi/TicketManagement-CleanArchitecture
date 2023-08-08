using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public async Task<MessageAttachment?> FindByIdAsync(long id)
        {
            return await _context.MessageAttachments
                .Include(a => a.Message)
                    .ThenInclude(m => m.Ticket)
                .FirstOrDefaultAsync(a => a.Id == id);
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
