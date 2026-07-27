using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management_Application.Dtos.LeaveAllocation.Validators;
using HR_Management_Application.Exceptions;
using HR_Management_Application.Features.LeaveAllocation.Requests.Commands;
using HR_Management_Application.Contracts.Persistence;
using MediatR;
using HR_Management_Application.Responses;

namespace HR_Management_Application.Features.LeaveAllocation.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler
        : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository);
            var res = await validator.ValidateAsync(request.CreateLeaveAllocationDto);
            if (res.IsValid != false)
            {
                // throw new ValidationException(res);
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = res.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }
            var leaveAllocation = _mapper.Map<HR_Management_Domain.LeaveAllocation>(request.CreateLeaveAllocationDto);
            leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
            // return leaveAllocation.Id;
            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveAllocation.Id;
            return response;
        }
    }
}