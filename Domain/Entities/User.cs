using Domain.Common;

namespace Domain.Entities
{
    public record User : BaseEntity
    {
        public string? Name { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
