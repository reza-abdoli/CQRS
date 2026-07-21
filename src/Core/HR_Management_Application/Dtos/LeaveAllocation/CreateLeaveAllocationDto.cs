using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Dtos.Common;

namespace HR_Management_Application.Dtos.LeaveAllocation
{
    public class CreateLeaveAllocationDto : BaseDto
    {

        public int NumberOfDays { get; set; }

        public int LeaveTypeId { get; set; }

        public int Priod { get; set; }
    }
}