namespace Application.Dtos.UserDtos
{
    public record LoginResponse
    {
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Token { get; set; }

    }
}
