using MediatR;

namespace Application.Features.UserFeatures.Login
{
    public record LoginRequest : IRequest<LoginResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
