using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management_Application.Dtos;
using HR_Management_Application.Dtos.LeaveRequest;
using HR_Management_Application.Features.LeaveRequest.Requests;
using HR_Management_Application.Persistence.Contracts;
using MediatR;

namespace HR_Management_Application.Features.LeaveRequest.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandler :
        IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequestList = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            return _mapper.Map<List<LeaveRequestListDto>>(leaveRequestList);
            // throw new NotImplementedException();
        }
    }
}