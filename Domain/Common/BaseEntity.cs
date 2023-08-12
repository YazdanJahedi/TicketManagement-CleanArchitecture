using System.ComponentModel.DataAnnotations;
namespace Domain.Common
{
    public record BaseEntity
    {
        public long Id { get; set; } 
        public DateTime CreationDate { get; set; }
    }
}
