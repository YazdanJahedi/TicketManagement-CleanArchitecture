using Application.Common.Exceptions;
using Application.Repository;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

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
            var attachment = await _messageAttachmentsRepository.FindById(request.attachmentId);
            if (attachment == null) throw new NotFoundException("attachment not found");

            var response = _mapper.Map<DownloadFileResponse>(attachment);
            return response;
        }
    }
}
