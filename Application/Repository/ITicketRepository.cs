using Application.Features.CreateTicket;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Application.Repository
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        public bool IsContextNull();
        public IQueryable<Ticket> FindAllByIsChecked(bool isChecked);
        public IQueryable<Ticket> FindAllById(long id);
        public Ticket FindById(long id);
        public void RemoveTicket(Ticket ticket);
        public void AddTicketAsync(Ticket ticket);
        public void UpdateTicketAsync(Ticket ticket);
    }
}
