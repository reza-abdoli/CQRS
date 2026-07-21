using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Dtos.Common;

namespace HR_Management_Application.Dtos.LeaveRequest
{
    public class ChangeLeaveRequestStateDto : BaseDto
    {
        public bool? Approved { get; set; }

    }
}