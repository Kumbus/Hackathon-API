using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos
{
    public class ConfirmEmailResponseDto
    {
        public bool IsSuccessfulConfirmation { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
