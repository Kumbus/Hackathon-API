using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISlotsRepository
    {
        public Task AddSlotAsync(ActivitySlot slot);
    }
}
