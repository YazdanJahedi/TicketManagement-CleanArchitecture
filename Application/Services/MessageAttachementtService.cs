using Application.Interfaces;
using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MessageAttachementtService : IMessageAttachmentService
    {
        private readonly IMessageAttachmentsRepository _messageAttachmentsRepository;
        public MessageAttachementtService(IMessageAttachmentsRepository messageAttachmentsRepository)
        {
            _messageAttachmentsRepository = messageAttachmentsRepository;
        }

        public async Task SaveMultipeAttachments(IEnumerable<IFormFile> files, long MessageId)
        {
            foreach (var file in files)
            {
                var stream = new MemoryStream();
                file.CopyTo(stream);

                var attachment = new MessageAttachment()
                {
                    MessageId = MessageId,
                    FileName = file.FileName,
                    FileData = stream.ToArray(),
                    CreationDate = DateTime.Now,
                };

                await _messageAttachmentsRepository.AddAsync(attachment);
            }
            await _messageAttachmentsRepository.SaveChangesAsync();
        }
    }
}
