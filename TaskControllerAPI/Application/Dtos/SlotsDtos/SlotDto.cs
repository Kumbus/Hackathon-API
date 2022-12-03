using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.ActivitySlot;

namespace Application.Dtos.SlotsDtos
{
    public record SlotDto
    {
        public Guid Id { get; set; }
        public Category CategoryOfActivity { get; set; }
        public DateTime Start { get; set; }
        public int QuartersNumber { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }
    }
}
