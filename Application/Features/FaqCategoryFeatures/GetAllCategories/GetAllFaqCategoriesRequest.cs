﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FaqCategoryFeatures.GetAllCategories
{
    public class GetAllFaqCategoriesRequest : IRequest<IEnumerable<GetAllFaqCategoriesResponse>>
    { }
}
