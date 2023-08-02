using Domain.Common;

namespace Domain.Entities
{
    public record User : BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string PasswordHash { get; set; }
        public required string PhoneNumber { get; set; }

        //public virtual ICollection<Ticket>? Tickets { get; set; }

    }
}
