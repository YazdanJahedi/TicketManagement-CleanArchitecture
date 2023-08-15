using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Context;


namespace Infrastructure.Repository
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext _context) : base(_context) { }

    }
}
