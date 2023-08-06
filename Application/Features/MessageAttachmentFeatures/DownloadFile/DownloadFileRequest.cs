using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MessageAttachmentFeatures.DownloadFile
{
    public class DownloadFileRequest : IRequest
    {
        public long attachmentId { get; set; }
        public DownloadFileRequest(long attachmentId)
        {
            this.attachmentId = attachmentId;
        }
    }
}
