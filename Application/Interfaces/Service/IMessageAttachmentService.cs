using Application.Dtos.MessageAttachmentDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IMessageAttachmentService
    {
        public Task SaveMultipeAttachments(IEnumerable<IFormFile> files, long MessageId);
        public Task<DownloadFileResponse> Download(long fileId);
    }
}
