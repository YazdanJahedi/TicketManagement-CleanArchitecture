﻿using Application.Dtos.UserDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.TicketDtos
{
    public record GetTicketsListResponse
    {
        public long Id { get; set; }
        public required GetUserInformationDto Creator { get; set; }
        public required string Title { get; set; }
        public DateTime? FirstResponseDate { get; set; }
        public required string Status { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
