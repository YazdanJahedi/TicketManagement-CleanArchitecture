using Application.Dtos.MessageAttachmentDtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class DownloadFileMapper : Profile
    {
        public DownloadFileMapper()
        {
            CreateMap<MessageAttachment, DownloadFileResponse>()
                .ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => "application/octet-stream"));
        }
    }
}
