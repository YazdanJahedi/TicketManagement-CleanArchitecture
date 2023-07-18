namespace Application.DTOs
{
    public record CreateUserDto
    {
        public string? Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
