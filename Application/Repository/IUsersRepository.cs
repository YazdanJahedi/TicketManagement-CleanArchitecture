using Domain.Entities;


namespace Application.Repository
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        public bool IsUserFoundByEmail(string email);
        public User FindUserByEmail(string email);
        public bool IsContextNull();
        public bool IsContextEmptyOrNull();
        public void AddUserAsync(User user);
    }
}
