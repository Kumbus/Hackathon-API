using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public record ActivitySlot : EntityBase
    {
        public enum Category
        {
            Job,
            Education,
            SelfImprovement,
            Entertainment,
            Sport,
            SocialLife
        }

        public Guid Id { get; set; }
        public Category CategoryOfActivity { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }

        public User User { get; set; }
        public IEnumerable<PlannedTask> Activities {get; set;}

        public ActivitySlot(Category categoryOfActivity, string name, DateTime start, DateTime end, string color)
        {
            Id = Guid.NewGuid();
            (CategoryOfActivity, Start, End, Name, Color) = (categoryOfActivity, start, end, name, color);
        }
    }
}
