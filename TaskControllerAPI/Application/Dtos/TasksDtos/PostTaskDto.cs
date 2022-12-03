using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.TasksDtos
{
    public record PostTaskDto
    {
        public int EstimatedMinutes { get; set; }
        public bool IsCompleted { get; set; }
        public int Priority { get; set; }
        public Guid? SlotId { get; set; }
        public string TaskName { get; set; }

        public string HexIdentificator { get; set; }
    }
}
