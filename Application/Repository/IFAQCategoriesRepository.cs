using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Repository
{
    public interface IFAQCategoriesRepository : IBaseRepository<FAQCategory>
    {
        public Task<IEnumerable<FAQCategory>> TestMethod();

    }
}
