using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class ResponseMessageDto
    {
        public required string Message { get; set; }
        public Guid ErrorReference { get; set; }
        public int ResultCount { get; set; }

        public override string ToString()
        {
            var message = new StringBuilder($"Message - {Message}");

            if (ResultCount > 0)
                message.Append($" ResultCount -{ResultCount}");

            if (ErrorReference != Guid.Empty)
                message.Append($"Error Reference- {ErrorReference}");

            return message.ToString();

        }
    }
}