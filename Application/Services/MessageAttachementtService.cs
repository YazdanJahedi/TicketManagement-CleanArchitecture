using Application.Common.Exceptions;
using Application.Dtos.MessageAttachmentDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;


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

        public async Task UploadRange(IEnumerable<IFormFile> files, long messageId)
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload", messageId.ToString());
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(path, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            var attachments = files.Select(f => 
                new MessageAttachment
                {
                    MessageId = messageId,
                    FileName = f.FileName,
                    Path = path,
                    CreationDate = DateTime.Now,
                }
            );

            await _messageAttachmentsRepository.AddRangeAsync(attachments);
        }
    }
}
