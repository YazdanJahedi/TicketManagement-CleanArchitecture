using Domain.Entities;


namespace Application.Repository
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        public User? FindByEmail(string email);
    }
}
