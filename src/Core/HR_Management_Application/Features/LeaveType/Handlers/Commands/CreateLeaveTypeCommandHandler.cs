using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management_Application.Dtos.LeaveType.Validators;
using HR_Management_Application.Features.LeaveType.Requests.Commands;
using HR_Management_Application.Persistence.Contracts;
using MediatR;

namespace HR_Management_Application.Features.LeaveType.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler
        : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveTypeValidator();
            var res = await validator.ValidateAsync(request.CreateLeaveTypeDto);
            if (res.IsValid != false)
            {
                throw new Exception();
            }

            var leaveType = _mapper.Map<HR_Management_Domain.LeaveType>(request.CreateLeaveTypeDto);
            leaveType = await _leaveTypeRepository.Add(leaveType);
            return leaveType.Id;
        }
    }
}