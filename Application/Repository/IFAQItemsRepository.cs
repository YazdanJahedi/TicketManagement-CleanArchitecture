﻿using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IFAQItemsRepository : IBaseRepository<FAQItem>
    {
        public bool IsContextNull();
        public IQueryable<FAQItem> FindAllByCategoryId(long id);
    }
}
