using Application.Dtos.MessageDtos;

namespace Application.Interfaces.Service
{
    public interface IMessageService
    {
        public Task Add(CreateMessageRequest request);
    }
}
