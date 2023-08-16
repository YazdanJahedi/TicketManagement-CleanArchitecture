﻿using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MessageAttachmentsRepository : BaseRepository<MessageAttachment>, IMessageAttachmentsRepository
    {
        public MessageAttachmentsRepository(ApplicationDbContext _context) : base(_context) { }

        public async Task AddRangeAsync(IEnumerable<MessageAttachment> messageAttachments)
        {
            await _context.MessageAttachments.AddRangeAsync(messageAttachments);
        }

        public async Task<MessageAttachment?> FindByIdAsync(long id)
        {
            return await _context.MessageAttachments
                .Include(a => a.Message)
                    .ThenInclude(m => m.Ticket)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
       
    }
}
