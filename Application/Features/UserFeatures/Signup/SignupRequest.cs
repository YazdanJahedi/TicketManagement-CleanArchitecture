using MediatR;

namespace Application.Features.UserFeatures.Signup
{
    public record SignupRequest : IRequest<SignupResponse>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
