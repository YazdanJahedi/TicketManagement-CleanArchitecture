using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public class UserQueries
    {
        public static bool IsUserFound(ApplicationDbContext _context, string email)
        {
            return (_context.Users?.Any(e => e.Email == email)).GetValueOrDefault();
        }

    }
}
