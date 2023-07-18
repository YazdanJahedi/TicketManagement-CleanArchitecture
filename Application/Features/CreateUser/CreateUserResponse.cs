namespace Application.Features.CreateUser
{
    public record CreateUserResponse
    {
        public string? Name { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
