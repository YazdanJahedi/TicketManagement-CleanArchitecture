using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repository;
using Moq;

namespace Test.RepositoryTests
{
    public class FAQItemsRepositoryTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly TicketsRepository _ticketRepository;

        public FAQItemsRepositoryTests()
        {
            _contextMock = new Mock<ApplicationDbContext>();
            _ticketRepository = new TicketsRepository(_contextMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntityToContext()
        {
            // Arrange
            var entity = new FAQItem();

            // Act
            await _ticketRepository.AddAsync(entity);

            // Assert
            _contextMock.Verify(c => c.Set<FAQItem>().AddAsync(entity), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            var entities = new List<FAQItem>
        {
            new FAQItem { Id = 1 },
            new FAQItem { Id = 2 },
            new FAQItem { Id = 3 }
        };

            var dbSetMock = entities.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Set<FAQItem>()).Returns(dbSetMock.Object);

            // Act
            var result = await _ticketRepository.GetAllAsync();

            // Assert
            Assert.Equal(entities.Count, result.Count());
            Assert.Equal(entities, result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldApplyNumberConditionAndIncludes()
        {
            // Arrange
            var entities = new List<FAQItem>
        {
            new FAQItem { Id = 1 },
            new FAQItem { Id = 2 },
            new FAQItem { Id = 3 }
        };

            var dbSetMock = entities.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Set<FAQItem>()).Returns(dbSetMock.Object);

            var condition = new Expression<Func<FAQItem, bool>>(e => e.Id == 2);
            var includes = new[] { "Category" };

            // Act
            var result = await _ticketRepository.GetAllAsync(2, condition, includes);

            // Assert
            _contextMock.Verify(c => c.Set<FAQItem>(), Times.Once);

            Assert.Equal(2, result.Count());
            Assert.Equal(2, result.First().Id);
            Assert.Equal(1, result.Last().Id);

            dbSetMock.Verify(d => d.OrderByDescending(It.IsAny<Expression<Func<FAQItem, DateTime>>>()), Times.Once);
            dbSetMock.Verify(d => d.Take(2), Times.Once);
            dbSetMock.Verify(d => d.Where(condition), Times.Once);
            dbSetMock.Verify(d => d.Include(includes[0]), Times.Once);
        }

        [Fact]
        public async Task GetByConditionAsync_ShouldReturnEntityByCondition()
        {
            // Arrange
            var entities = new List<FAQItem>
        {
            new FAQItem { Id = 1 },
            new FAQItem { Id = 2 },
            new FAQItem { Id = 3 }
        };

            var dbSetMock = entities.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Set<FAQItem>()).Returns(dbSetMock.Object);

            var condition = new Expression<Func<FAQItem, bool>>(e => e.Id == 2);
            var includes = new[] { "Category" };

            // Act
            var result = await _ticketRepository.GetByConditionAsync(condition, includes);

            // Assert
            _contextMock.Verify(c => c.Set<FAQItem>(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);

            dbSetMock.Verify(d => d.FirstOrDefaultAsync(condition), Times.Once);
            dbSetMock.Verify(d => d.Include(includes[0]), Times.Once);
        }
    }
}
