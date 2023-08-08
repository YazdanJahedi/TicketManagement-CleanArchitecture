using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMessageAttachmentService
    {
        public Task SaveAttachment(IFormFile file, long Messageid);
        public Task SaveMultipeAttachments(IEnumerable<IFormFile> files, long MessageId);
    }
}
