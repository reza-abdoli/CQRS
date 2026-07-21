using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace HR_Management_Application.Dtos.LeaveType.Validators
{
    public class CreateLeaveTypeValidator : AbstractValidator<CreateLeaveTypeDto>
    {
        public CreateLeaveTypeValidator()
        {
            Include(new ILeaveTypeValidator());
        }
    }
}