using MediatR;

namespace Application.DTOs.UserDtos
{
    public record LoginRequest : IRequest<LoginResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
