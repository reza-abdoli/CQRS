using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Dtos.Common;

namespace HR_Management_Application.Dtos.LeaveType
{
    public class CreateLeaveTypeDto : BaseDto,ILeaveTypeDto
    {
        public string Name { get; set; }
        public int DefaultDay { get; set; }
    }
}