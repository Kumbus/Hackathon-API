using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.ActivitySlot;

namespace Application.AdditionalStructures
{
    public class SlotWithTasks
    {
        public Guid Id { get; set; }
        public Category CategoryOfActivity { get; set; }
        public DateTime Start { get; set; }
        public int QuartersNumber { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }

        public List<Guid> TasksIds { get; set; }

        public SlotWithTasks(SlotWithTasks slot) { 
            Id = slot.Id;  
            CategoryOfActivity= slot.CategoryOfActivity;
            Start= slot.Start;
            QuartersNumber = slot.QuartersNumber;
            UserId = slot.UserId;
            Color = slot.Color;
            TasksIds = new List<Guid>();
            foreach(var id in slot.TasksIds)
            {
                TasksIds.Add(id);
            }
        }

        public SlotWithTasks()
        {
        }
    }
}
