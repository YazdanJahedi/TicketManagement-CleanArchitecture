using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Repository
{
    public interface IFAQCategoriesRepository : IBaseRepository<FAQCategory>
    {
        Task<ActionResult<IEnumerable<FAQCategory>>> GetFAQCategories();
    }
}
