using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AdditionalStructures
{
    public class Weekday
    {
        public List<SlotWithTasks> slotWithTasks { get; set; } = new List<SlotWithTasks>();

        public Weekday(List<SlotWithTasks> weekday) { 
            foreach(var slot in weekday){
                slotWithTasks.Add(new SlotWithTasks(slot));
            }
        }

        public Weekday()
        {
        }
    }
}
