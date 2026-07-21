using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_Application.Dtos.LeaveRequest
{
    public class UpdateLeaveRequestDto
    {
        public DateTime StartDate { get; set; }
        public string RequestComments { get; set; }

        public DateTime EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public bool Cancelled { get; set; }
    }
}