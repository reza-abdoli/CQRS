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

namespace HR_Management_Application.Features.LeaveRequest.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {

            var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository);
            var res = await validator.ValidateAsync(request.UpdateLeaveRequestDto);
            if (res.IsValid != false)
            {
                throw new ValidationException(res);
            }
            var leaveRequest = await _leaveRequestRepository.Get(request.Id);
            if (request.UpdateLeaveRequestDto != null)
            {
                // var leaveRequest = await _leaveRequestRepository.Get(request.LeaveRequestDto.Id);
                _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
                await _leaveRequestRepository.Update(leaveRequest);
                return Unit.Value;
            }
            else if (request.ChangeLeaveRequestStateDto != null)
            {
                await _leaveRequestRepository.ChangeRequestStatus(leaveRequest, request.ChangeLeaveRequestStateDto.Approved);
            }
            return Unit.Value;
        }
    }
}