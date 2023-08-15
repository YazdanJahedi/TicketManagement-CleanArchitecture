using Application.Dtos.MessageAttachmentDtos;
using Application.Dtos.MessageDtos;
using Application.Dtos.TicketDtos;
using Application.Dtos.UserDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers.TicketMappers
{
    public class GetTicketMapper : Profile
    {
        public GetTicketMapper()
        {

            CreateMap<Ticket, GetTicketResponse>()
               .ForMember(dest => dest.Messages, opt => opt.MapFrom(src =>
                   src.Messages!.Select(m => new MessageDetailsDto
                   {
                       Text = m.Text,
                       Creator = new UserDetailsDto
                       {
                           Name = m.Creator!.Name,
                           PhoneNumber = m.Creator.PhoneNumber,
                           Email = m.Creator.Email,
                           Role = m.Creator.Role
                       },
                       CreationDate = m.CreationDate,
                       Attachments = m.Attachments!.Select(x =>
                       new AttachmentDetailsDto
                       {
                           Id = x.Id,
                           FileName = x.FileName
                       })
                   })))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
