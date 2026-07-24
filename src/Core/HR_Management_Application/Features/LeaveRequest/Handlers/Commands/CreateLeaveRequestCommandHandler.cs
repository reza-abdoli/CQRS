using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management_Application.Dtos.LeaveRequest.Validators;
using HR_Management_Application.Exceptions;
using HR_Management_Application.Features.LeaveRequest.Requests.Commands;
using HR_Management_Application.Contracts.Persistence;
using MediatR;
using HR_Management_Application.Contracts.Infrastructure;
using HR_Management_Application.Responses;
using HR_Management_Application.Models;

namespace HR_Management_Application.Features.LeaveRequest.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler
        : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper,
            IEmailSender emailSender)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var res = await validator.ValidateAsync(request.CreateLeaveRequestDto);
            if (res.IsValid != false)
            {
                throw new ValidationException(res);
            }
            var leaveRequest = _mapper.Map<HR_Management_Domain.LeaveRequest>(request.CreateLeaveRequestDto);
            leaveRequest = await _leaveRequestRepository.Add(leaveRequest);

            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Creation Successful",
                Id = leaveRequest.Id
            };

            var email = new Email
            {
                To = "reza@ab.com",
                Subject = "Leave Request Submitted",
                Body = $"Your leave request for {request.CreateLeaveRequestDto.StartDate} " +
                       $"to {request.CreateLeaveRequestDto.EndDate} " +
                       $"has been submitted"
            };

            // because we are using external service, we don't know what is going to happen, 
            // so we will use try catch block to handle any exception that may occur
            try
            {
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                //log
            }

            return response;
        }
    }
}