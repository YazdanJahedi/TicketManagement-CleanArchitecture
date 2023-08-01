using Application.DTOs.UserDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MessageDtos
{
    public record GetMessageResponse
    {
        public required string Text { get; set; }
        public required GetUserInformationRequest Creator { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
