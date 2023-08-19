using Application.Common.Exceptions;
using Application.Dtos.MessageAttachmentDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class MessageAttachementtService : IMessageAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _userService;
        public MessageAttachementtService(IUnitOfWork unitOfWork, IAuthService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<DownloadResponse> Download(long fileId)
        {
            var claims = _userService.GetClaims();

            var attachment = await _unitOfWork.MessageAttachmentsRepository.FindByIdAsync(fileId);
            if (attachment == null || (claims.Role == "User" && attachment.Message!.Ticket!.CreatorId != claims.Id)) throw new NotFoundException("attachment not found");

            var filePath = Path.Combine(attachment.Path, attachment.FileName);

            var data = await File.ReadAllBytesAsync(filePath);

            var response = new DownloadResponse
            {
                FileName = attachment.FileName,
                Data = data,
                MimeType = "application/octet-stream",
            };
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

            await _unitOfWork.MessageAttachmentsRepository.AddRangeAsync(attachments);
        }
    }
}
