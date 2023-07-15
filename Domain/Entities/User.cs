using Domain.Common;

namespace Domain.Entities
{
    public record User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
