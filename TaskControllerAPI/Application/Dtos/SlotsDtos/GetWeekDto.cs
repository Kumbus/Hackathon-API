using Application.AdditionalStructures;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.ActivitySlot;

namespace Application.Dtos.SlotsDtos
{
    public class GetWeekDto
    {
        public List<Weekday> Week { get; set; }
    }
}
