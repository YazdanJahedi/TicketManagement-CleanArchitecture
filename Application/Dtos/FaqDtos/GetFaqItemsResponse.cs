﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.FaqDtos
{
    public record GetFaqItemsResponse
    {
        public required long CategoryId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}