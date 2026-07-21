using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HR_Management_Application.Persistence.Contracts;

namespace HR_Management_Application.Dtos.LeaveRequest.Validators
{
    public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository; 
        public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(q => q.StartDate)
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .LessThan(q => q.EndDate)
                .WithMessage("{PropertyName} must be before {ComparisonValue}.");

            RuleFor(q => q.EndDate)
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(q => q.StartDate)
                .WithMessage("{PropertyName} must be after {ComparisonValue}.");

            RuleFor(q => q.LeaveTypeId)
                .NotNull()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var res = await _leaveTypeRepository.Exists(id);
                    return !res;
                })
                .WithMessage("{PropertyName} does not exist");
        }
    }
}