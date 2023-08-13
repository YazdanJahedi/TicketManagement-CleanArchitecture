using MediatR;

namespace Application.Dtos.UserDtos
{
    public record LoginRequest : IRequest<LoginResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
