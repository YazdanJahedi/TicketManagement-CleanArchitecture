using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.FaqCategoryDto
{
    public record GetFaqCategoriesResponse
    {
        public required long Id { get; set; }
        public required string CategoryName { get; set; }
    }
}
