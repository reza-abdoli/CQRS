using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Dtos;
using HR_Management_Application.Dtos.LeaveRequest;
using MediatR;

namespace HR_Management_Application.Features.LeaveRequest.Requests.Commands
{
    public class CreateLeaveRequestCommand : IRequest<int>
    {
        public CreateLeaveRequestDto CreateLeaveRequestDto { get; set; }
    }
}