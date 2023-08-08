using Application.Common.Exceptions;
using Application.Repository;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.MessageAttachmentFeatures.DownloadFile
{

    public class DownloadFileHandler : IRequestHandler<DownloadFileRequest, DownloadFileResponse>
    {

        private readonly IMessageAttachmentsRepository _messageAttachmentsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public DownloadFileHandler(IMessageAttachmentsRepository messageAttachmentsRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _messageAttachmentsRepository = messageAttachmentsRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<DownloadFileResponse> Handle(DownloadFileRequest request, CancellationToken cancellationToken)
        {
            var idString = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userId = Convert.ToInt64(idString);
            var role = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.Role).Value;

            var attachment = await _messageAttachmentsRepository.FindByIdAsync(request.attachmentId);
            if (attachment == null || (role == "User" && attachment.Message!.Ticket!.CreatorId != userId)) throw new NotFoundException("attachment not found");

            var response = _mapper.Map<DownloadFileResponse>(attachment);
            return response;
        }
    }
}
