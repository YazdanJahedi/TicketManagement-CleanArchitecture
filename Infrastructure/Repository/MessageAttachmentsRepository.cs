using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
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
    }
}
