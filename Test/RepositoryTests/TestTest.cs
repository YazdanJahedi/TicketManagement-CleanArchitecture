/*using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

public class UsersRepositoryTests
{
    private readonly Mock<ApplicationDbContext> _mockContext;
    private readonly UsersRepository _userRepository;

    public UsersRepositoryTests()
    {
        _mockContext = new Mock<ApplicationDbContext>();
        _userRepository = new UsersRepository(_mockContext.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntityToContext()
    {
        // Arrange
        var user = new User { Id = 1, Name = "John", Email = "j@j.j", PasswordHash = "pw", PhoneNumber = "0123", Role = "role" };

        // Act
        await _userRepository.AddAsync(user);

        // Assert
        _mockContext.Verify(c => c.Set<User>().Add(user), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Name = "John" , Email = "j@j.j", PasswordHash="pw", PhoneNumber = "0123", Role = "role"},
            new User { Id = 2, Name = "Jane", Email = "j@j.j", PasswordHash="pw", PhoneNumber = "013", Role = "role" },
            new User {Id = 3, Name = "Alice", Email = "a@a.a", PasswordHash = "pw", PhoneNumber = "0123", Role = "role"}
        };

        var mockDbSet = GetMockDbSet(users);

        _mockContext.Setup(c => c.Set<User>()).Returns(mockDbSet.Object);

        // Act
        var result = await _userRepository.GetAllAsync();

        // Assert
        Assert.Equal(users.Count, result.Count());
        Assert.Equal(users, result);
    }

    [Fact]
    public async Task GetByConditionAsync_ShouldReturnUserMatchingCondition()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Name = "John" , Email = "j@j.j", PasswordHash="pw", PhoneNumber = "0123", Role = "role"},
            new User { Id = 2, Name = "Jane", Email = "j@j.j", PasswordHash="pw", PhoneNumber = "013", Role = "role" },
            new User {Id = 3, Name = "Alice", Email = "a@a.a", PasswordHash = "pw", PhoneNumber = "0123", Role = "role"}
        };

        var mockDbSet = GetMockDbSet(users);

        _mockContext.Setup(c => c.Set<User>()).Returns(mockDbSet.Object);

        // Act
        var result = await _userRepository.GetByConditionAsync(u => u.Name == "John");

        // Assert
        Assert.Equal(users.First(), result);
    }

    private Mock<DbSet<T>> GetMockDbSet<T>(List<T> data) where T : class
    {
        var queryableData = data.AsQueryable();
        var mockDbSet = new Mock<DbSet<T>>();

        mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

        return mockDbSet;
    }
}*/