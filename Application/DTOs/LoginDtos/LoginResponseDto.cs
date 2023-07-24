namespace Application.DTOs.LoginDtos
{
    public record LoginResponseDto
    {
        public string? Email { get; set; }
        public string? Role { get; set; }
        public required string Token { get; set; }

    }
}
