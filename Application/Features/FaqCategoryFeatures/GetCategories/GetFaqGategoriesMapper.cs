using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FaqCategoryFeatures.GetAllCategories
{
    public class GetFaqGategoriesMapper : Profile
    {
        public GetFaqGategoriesMapper() 
        {
            CreateMap<FAQCategory, GetFaqCategoriesResponse>();
        }
    }
}
