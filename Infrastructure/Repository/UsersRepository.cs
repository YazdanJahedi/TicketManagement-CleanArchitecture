using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repository
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext _context) : base(_context) { }

        public User? FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(e => e.Email == email);
        }


    }
}
