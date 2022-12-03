using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.ActivitySlot;

namespace Application.Dtos.SlotsDtos
{
    public record UpdateSlotDto
    {
        public Category CategoryOfActivity { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Color { get; set; }
        public string HexIdentificator { get; set; }
    }
}
