﻿using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Application.Repository
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        public Task<IEnumerable<Ticket>> GetFirstOnesByCreatorIdAsync(long creatorId, int number);
        public Task<IEnumerable<Ticket>> GetAllFirstOnesAsync(int number);
        public Task<Ticket?> FindByIdAsync(long id, bool loadCompelete = true);
        public Task RemoveAsync(Ticket ticket);
        public Task UpdateAsync(Ticket ticket);

    }
}
