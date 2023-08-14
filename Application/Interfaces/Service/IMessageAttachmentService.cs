using Application.Dtos.MessageAttachmentDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IMessageAttachmentService
    {
        public Task UploadRange(IEnumerable<IFormFile> files, long MessageId);
        public Task<DownloadResponse> Download(long fileId);
    }
}
