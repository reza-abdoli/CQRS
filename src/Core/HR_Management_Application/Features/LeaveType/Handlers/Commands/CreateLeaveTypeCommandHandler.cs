using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management_Application.Dtos.LeaveType.Validators;
using HR_Management_Application.Exceptions;
using HR_Management_Application.Features.LeaveType.Requests.Commands;
using HR_Management_Application.Contracts.Persistence;
using HR_Management_Application.Responses;
using MediatR;

namespace HR_Management_Application.Features.LeaveType.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler
        : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            // in this case, im not using the validator to validate the request, i used response to return the validation errors to the client
            var validator = new CreateLeaveTypeDtoValidator();
            var res = await validator.ValidateAsync(request.CreateLeaveTypeDto);
            if (res.IsValid == false)
            {
                // throw new ValidationException(res);
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = res.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var leaveType = _mapper.Map<HR_Management_Domain.LeaveType>(request.CreateLeaveTypeDto);
            leaveType = await _leaveTypeRepository.Add(leaveType);
            // return leaveType.Id;
            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveType.Id;
            return response;
        }
    }
}