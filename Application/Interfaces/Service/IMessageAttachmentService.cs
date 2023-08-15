using Application.Dtos.MessageAttachmentDtos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Service
{
    public interface IMessageAttachmentService
    {
        public Task UploadRange(IEnumerable<IFormFile> files, long MessageId);
        public Task<DownloadResponse> Download(long fileId);
    }
}
