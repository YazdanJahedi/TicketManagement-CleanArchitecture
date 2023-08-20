using Application.Dtos.MessageAttachmentDtos;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Service
{
    public interface IMessageAttachmentService
    {
        public Task UploadRange(IEnumerable<IFormFile> files, Message message);
        public Task<DownloadResponse> Download(long fileId);
    }
}
