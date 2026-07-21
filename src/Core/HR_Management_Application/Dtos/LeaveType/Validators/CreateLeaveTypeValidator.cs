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
            RuleFor(r => r.Name).NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull()
            .MaximumLength(30).WithMessage("maximum length reached");

            RuleFor(r => r.DefaultDay).NotNull().WithMessage("{PropertyName} required")
            .GreaterThan(0).WithMessage("{PropertyName} must greater than 0")
            .LessThan(100);
        }
    }
}