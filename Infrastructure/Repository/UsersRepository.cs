using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repository
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext _context)
            :base(_context)
        {
        }

        public async void AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public User FindUserByEmail(string email)
        {
            // nullable
            return _context.Users.FirstOrDefault(e => e.Email == email);
        }

        public bool IsContextEmptyOrNull()
        {
            return _context.Users.IsNullOrEmpty();
        }

        public bool IsContextNull()
        {
            return _context.Users == null;
        }

        public bool IsUserFoundByEmail(string email)
        {
            return (_context.Users?.Any(e => e.Email == email)).GetValueOrDefault();
        }
    }
}
