using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management_Application.Dtos;
using HR_Management_Application.Features.LeaveAllocation.Requests;
using HR_Management_Application.Contracts.Persistence;
using MediatR;

namespace HR_Management_Application.Features.LeaveAllocation.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler :
        IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository leaveAllocationRepository,
            IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }
        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            // var leaveAllocationList = await _leaveAllocationRepository.GetAll();
            // return _mapper.Map<List<LeaveAllocationDto>>(leaveAllocationList);
            throw new NotImplementedException();
        }
    }
}