/*using Moq;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    [TestClass]
    public class TicketRepositoryTest
    {
        private Ticket[] _tickets;
        private Mock<DbSet<Ticket>> _mockSet;
        private TicketsRepository _repo;

        [TestInitialize]
        public void Initialize()
        {
            _tickets = new[] { CreateTicket(1), CreateTicket(2) };

            _mockSet = new Mock<DbSet<Ticket>>();
            _mockSet.Setup(m => m.Find(It.IsAny<long>())).Returns<long>(id => _tickets.FirstOrDefault(t => t.Id == id));

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Tickets).Returns(_mockSet.Object);

            _repo = new TicketsRepository(mockContext.Object);
        }

        private Ticket CreateTicket(int v)
        {
            return new Ticket { Id = v, CreatorId = 1, Description="d", Title = "t" };
        }

        [TestMethod]
        public void GetAll_ReturnsAllItems()
        {
            var result = _repo.GetAll().ToList();

            CollectionAssert.AreEqual(_tickets, result);
        }

        [TestMethod]
        public void FindById_ReturnsExpectedItem()
        {
            var expected = _tickets[0];

            var result = _repo.FindById(expected.Id);

            Assert.AreSame(expected, result);
        }

        [TestMethod]
        public void Add_IncreasesCount()
        {
            var originalCount = _repo.GetAll().Count();

            var ticket = CreateTicket(3);

            _repo.Add(ticket);

            var newCount = _repo.GetAll().Count();

            Assert.AreEqual(originalCount + 1, newCount);
        }

        [TestMethod]
        public void Remove_ValidatesExistingItem()
        {
            var invalidTicket = CreateTicket(-1);

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                _repo.Remove(invalidTicket);
            });
        }
    }
}


// */