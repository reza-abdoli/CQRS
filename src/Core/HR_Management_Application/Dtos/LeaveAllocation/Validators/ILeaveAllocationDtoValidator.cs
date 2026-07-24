using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HR_Management_Application.Contracts.Persistence;

namespace HR_Management_Application.Dtos.LeaveAllocation.Validators
{
    public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");

            RuleFor(p => p.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");

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