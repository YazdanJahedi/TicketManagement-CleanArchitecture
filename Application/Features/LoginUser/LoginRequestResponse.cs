namespace Application.Features.LoginUser
{
    public record LoginRequestResponse
    {
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Token { get; set;}

    }
}
