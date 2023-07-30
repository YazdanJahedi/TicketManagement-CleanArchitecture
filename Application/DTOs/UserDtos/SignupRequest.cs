﻿using MediatR;

namespace Application.DTOs.UserDtos
{
    public record SignupRequest : IRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
