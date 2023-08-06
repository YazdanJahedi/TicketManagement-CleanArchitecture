using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MessageAttachmentFeatures.UploadFile
{
    public record UploadFileRequest : IRequest
    {
        public required IFormFile FileDetails { get; set; }

    }
}
