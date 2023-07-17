using Application.Repository;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class TicketsRepository : BaseRepository<Ticket>, ITicketsRepository
    {
        public TicketsRepository(ApplicationDbContext _context)
            : base(_context)
        {
        }

        public async void AddTicketAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Ticket> FindAllById(long id)
        {
            return _context.Tickets.Where(a => a.Id == id);
        }

        public IQueryable<Ticket> FindAllByIsChecked(bool isChecked)
        {
            return _context.Tickets.Where(a => a.IsChecked == isChecked);
        }

        public Ticket FindById(long id)
        {
            // nullable
            return _context.Tickets.Find(id);
        }

        public bool IsContextNull()
        {
            return _context.Tickets == null;
        }

        public void RemoveTicket(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
        }

        public async void UpdateTicketAsync(Ticket ticket)
        {
            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
