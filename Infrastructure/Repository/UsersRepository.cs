using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Web.Helpers;

namespace Infrastructure.Repository
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext _context) : base(_context) { }

    }
}
