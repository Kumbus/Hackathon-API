using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public record PlannedTask : EntityBase
    {
        public Guid Id { set; get; }
        public int EstimatedMinutes { get; set; }
        public bool IsCompleted { get; set; }
        public int Priority { get; set; }
        public Guid? SlotId {get; set; }
        public string UserId { get; set; }
        public string TaskName { get; set; }

        public ActivitySlot Slot { get; set; }
        public User User { get; set; }

        public PlannedTask(int estimatedMinutes, bool isCompleted, int priority, Guid? slotId, string userId, string taskName)
        {
            Id = Guid.NewGuid();
            EstimatedMinutes = estimatedMinutes;
            IsCompleted = isCompleted;
            Priority = priority;
            SlotId = slotId;
            UserId = userId;
            TaskName = taskName;
        }
    }
}
