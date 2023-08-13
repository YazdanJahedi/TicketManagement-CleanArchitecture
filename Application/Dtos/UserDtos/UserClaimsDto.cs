using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos
{
    public record UserClaimsDto
    {
        public required long Id { get; set; }
        public required string Role { get; set; }
    }
}
