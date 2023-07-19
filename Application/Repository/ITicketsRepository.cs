﻿using Application.Features.CreateTicket;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Application.Repository
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        public IEnumerable<Ticket> FindAllByCreatorId(long creatorId);
        public Ticket? FindById(long id);
        public void Remove(Ticket ticket);
    }
}
