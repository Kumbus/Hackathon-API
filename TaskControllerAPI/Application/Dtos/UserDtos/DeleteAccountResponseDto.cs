using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos
{
    public class DeleteAccountResponseDto
    {
        public bool IsSuccessfulDelete { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
