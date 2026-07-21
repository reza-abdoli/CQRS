using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_Application.Dtos.LeaveType
{
    public interface ILeaveTypeDto
    {
        public string Name { get; set; }
        public int DefaultDay { get; set; }
    }
}