using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

public class UsersRepositoryTests
{
    private ApplicationDbContext _dbContext;
    private UsersRepository _userRepository;

    public UsersRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _userRepository = new UsersRepository(_dbContext);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntityToDatabase()
    {
        ClearDb();

        var user = new User { Name = "a", Email = "a@a.a", PasswordHash = "pass", PhoneNumber = "1234", Role = "role" };


        await _userRepository.AddAsync(user);
        await _dbContext.SaveChangesAsync();


        var addedUser = await _dbContext.Users.FindAsync(user.Id);
        Assert.NotNull(addedUser);
    }

    [Fact]
    public async Task GetByConditionAsync_ShouldReturnMatchingEntity()
    {
        ClearDb();

        var user = new User { Name = "a", Email = "a@a.a", PasswordHash = "pass", PhoneNumber = "1234", Role = "role" };
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();


        var result = await _userRepository.GetByConditionAsync(u => u.Id == user.Id);


        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
    }

    internal void ClearDb()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}