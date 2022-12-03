using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class TaskValidator : AbstractValidator<PlannedTask>
    {
        public TaskValidator()
        {
            RuleFor(a => a.Priority).LessThan(6).GreaterThanOrEqualTo(1);
            RuleFor(a => a.EstimatedMinutes).GreaterThanOrEqualTo(15).LessThan(1440);
        }
    }
}
