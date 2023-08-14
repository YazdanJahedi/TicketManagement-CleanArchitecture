using Application.Common.Exceptions;
using Application.Dtos.MessageAttachmentDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MessageAttachementtService : IMessageAttachmentService
    {
        private readonly IMessageAttachmentsRepository _messageAttachmentsRepository;
        private readonly IAuthService _userService;
        private readonly IMapper _mapper;
        public MessageAttachementtService(IMessageAttachmentsRepository messageAttachmentsRepository, IAuthService userService,
                                IMapper mapper)
        {
            _messageAttachmentsRepository = messageAttachmentsRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<DownloadFileResponse> Download(long fileId)
        {
            var claims = _userService.GetClaims();

            var attachment = await _messageAttachmentsRepository.FindByIdAsync(fileId);
            if (attachment == null || (claims.Role == "User" && attachment.Message!.Ticket!.CreatorId != claims.Id)) throw new NotFoundException("attachment not found");

            var response = _mapper.Map<DownloadFileResponse>(attachment);
            return response;
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
            //await _messageAttachmentsRepository.SaveChangesAsync();
        }
    }
}
