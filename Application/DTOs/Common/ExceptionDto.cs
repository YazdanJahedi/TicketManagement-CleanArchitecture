using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Common
{
    public record ExceptionDto
    {
        public string Type { get; set; }
        public string Message { get; set; }

        public ExceptionDto(string type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
