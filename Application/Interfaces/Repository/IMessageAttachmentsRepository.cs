using Domain.Entities;

namespace Application.Interfaces.Repository
{
    public interface IMessageAttachmentsRepository : IBaseRepository<MessageAttachment>
    {
        public Task<MessageAttachment?> FindByIdAsync(long id);
        public Task AddRangeAsync(IEnumerable<MessageAttachment> messageAttachments);
    }
}
