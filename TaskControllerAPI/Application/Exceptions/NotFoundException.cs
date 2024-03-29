﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message) : base(message)
        {

        }
    }

    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid userId) : base($"User with id {userId} doesn't exist.")
        {
        }

        public UserNotFoundException(string userId) : base($"User with id {userId} doesn't exist.")
        {
        }
    }
    public sealed class SlotNotFoundException : NotFoundException
    {

        public SlotNotFoundException(Guid slotId) : base($"Slot with id {slotId} doesn't exist.")
        {
        }
    }

    public sealed class TaskNotFoundException : NotFoundException
    {

        public TaskNotFoundException(Guid taskId) : base($"Task with id {taskId} doesn't exist.")
        {
        }
    }
}
