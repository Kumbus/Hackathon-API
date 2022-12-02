using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public record EntityBase
    {
        public DateTime CreatedAt { set; get; }
        public DateTime? LastModified { set; get; }
    }
}
