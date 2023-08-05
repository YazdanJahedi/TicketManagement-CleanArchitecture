using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public record MessageAttachment : BaseEntity
    {
        [ForeignKey("Message")]
        public required long MessageId { get; set; }
        public virtual Message? Message { get; set; }

        public required string Name { get; set; }
        public required byte[] FileData { get; set; }

    }      
}
