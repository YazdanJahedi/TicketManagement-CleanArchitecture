using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDtos
{
    public record GetTicketsListResponse
    {
        // NOT USED YET
        public bool? IsChecked { get; set; }
        //public DateTime? CloseDate { get; set; }
        // status  -> checked / not checked / closed
        // number of messages
    }
}
