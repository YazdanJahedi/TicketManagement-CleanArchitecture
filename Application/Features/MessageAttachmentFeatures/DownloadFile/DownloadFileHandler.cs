using Application.Common.Exceptions;
using Application.Repository;
using MediatR;


namespace Application.Features.MessageAttachmentFeatures.DownloadFile
{

    public class DownloadFileHandler : IRequestHandler<DownloadFileRequest>
    {

        private readonly IMessageAttachmentsRepository _messageAttachmentsRepository;
        public DownloadFileHandler(IMessageAttachmentsRepository messageAttachmentsRepository)
        {
            _messageAttachmentsRepository = messageAttachmentsRepository;
        }

        public async Task<Unit> Handle(DownloadFileRequest request, CancellationToken cancellationToken)
        {
            var attachment = await _messageAttachmentsRepository.FindById(request.attachmentId);
            if (attachment == null) throw new NotFoundException("attachment not found");

            var data = new MemoryStream(attachment.FileData);
            var downloadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "DownloadedFiles");
            var downloadPath = Path.Combine(downloadFolderPath, attachment.FileName);

            Directory.CreateDirectory(downloadFolderPath); // Create directory if does not exist

            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await data.CopyToAsync(fileStream);
            }

            return Unit.Value;
        }
    }
}
