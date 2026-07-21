using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Dtos.Common;

namespace HR_Management_Application.Dtos.LeaveRequest
{
    public class UpdateLeaveRequestDto : BaseDto, ILeaveRequestDto
    {
        public DateTime StartDate { get; set; }
        public string RequestComments { get; set; }

        public DateTime EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public bool Cancelled { get; set; }
    }
}