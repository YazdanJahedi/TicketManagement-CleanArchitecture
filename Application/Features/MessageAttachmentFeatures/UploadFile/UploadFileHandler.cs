using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MessageAttachmentFeatures.UploadFile
{

    public class UploadFileHandler : IRequestHandler<UploadFileRequest>
    {
        private readonly IMessageAttachmentsRepository _messageAttachmentsRepository;

        public UploadFileHandler (IMessageAttachmentsRepository messageAttachmentsRepository)
        {
            _messageAttachmentsRepository = messageAttachmentsRepository;
        }

        public async Task<Unit> Handle(UploadFileRequest request, CancellationToken cancellationToken)
        {
            
            var attachment = new MessageAttachment()
            {
                FileName = request.FileDetails.FileName,
            };

            using (var stream = new MemoryStream())
            {
                request.FileDetails.CopyTo(stream);
                attachment.FileData = stream.ToArray();
            }

            await _messageAttachmentsRepository.AddAsync(attachment);
            return Unit.Value;             
        }
    }
}
