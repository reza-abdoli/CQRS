using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Dtos.Common;
using HR_Management_Application.Dtos.LeaveType;

namespace HR_Management_Application.Dtos
{
    public class LeaveTypeDto : BaseDto,ILeaveTypeDto
    {
        public string Name { get; set; }
        public int DefaultDay { get; set; }
    }
}