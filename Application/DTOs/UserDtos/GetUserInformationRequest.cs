﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserDtos
{
    public class GetUserInformationRequest
    {
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }

    }
}