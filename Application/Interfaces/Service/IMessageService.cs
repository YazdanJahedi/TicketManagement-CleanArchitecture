using Application.Dtos.MessageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IMessageService
    {
        public Task AddMessage(CreateMessageRequest request);
    }
}
