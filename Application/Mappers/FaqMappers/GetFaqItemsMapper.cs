﻿using Application.Dtos.FaqDtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers.FaqMappers
{
    public class GetFaqItemsMapper : Profile
    {
        public GetFaqItemsMapper()
        {
            CreateMap<FAQItem, GetFaqItemsResponse>();
        }
    }
}
