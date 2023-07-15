using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Repository
{
    public interface IFAQRepository : IBaseRepository<FAQCategory>
    {
        Task<ActionResult<IEnumerable<FAQCategory>>> GetFAQCategories();
        ActionResult<IEnumerable<FAQItem>> GetFAQItems(int id);
    }
}
