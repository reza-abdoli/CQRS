using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace HR_Management_Application.Dtos.LeaveType.Validators
{
    public class UpdateLeaveTypeValidator  : AbstractValidator<LeaveTypeDto>
    {
        public UpdateLeaveTypeValidator()
        {
            Include(new ILeaveTypeValidator());

            RuleFor(r => r.Id).NotNull().WithMessage("{PropertyName} required");
        }
    }
}