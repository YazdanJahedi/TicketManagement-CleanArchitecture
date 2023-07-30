using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    internal class ValidationErrorException : Exception
    {
        public ValidationErrorException(string message):base(message) { }
    }
}
