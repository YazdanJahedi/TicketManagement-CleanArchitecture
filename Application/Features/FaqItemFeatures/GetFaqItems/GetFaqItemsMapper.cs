using Application.Features.FaqItemFeatures.GetAllFaqItems;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FaqItemFeatures.GetFaqItems
{
    public class GetFaqItemsMapper : Profile
    {
        public GetFaqItemsMapper() 
        {
            CreateMap<FAQItem, GetFaqItemsResponse > ();
        }
    }
}
