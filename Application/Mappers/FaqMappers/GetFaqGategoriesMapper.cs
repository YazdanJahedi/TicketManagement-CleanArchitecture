using Application.Dtos.FaqDtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers.FaqMappers
{
    public class GetFaqGategoriesMapper : Profile
    {
        public GetFaqGategoriesMapper()
        {
            CreateMap<FAQCategory, GetFaqCategoriesResponse>();
        }
    }
}
