using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public IEnumerable<ActivitySlot> Slots { get; set; }
        public IEnumerable<PlannedTask> Tasks { get; set; }

        public User( string userName) : base(userName)
        {
            UserName = userName;
        }
    }
}
