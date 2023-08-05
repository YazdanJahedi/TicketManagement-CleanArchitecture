using Domain.Entities;


namespace Application.Repository
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        public Task<User?> FindByEmailAsync(string email);
        public Task<User?> FindByNameAsync(string username);
    }
}
