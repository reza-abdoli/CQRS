using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Contracts.Persistence;
using HR_Management_Domain;

namespace HR_Management_Persistence.Repository
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly LeaveManagementDbContext _context;
        public LeaveTypeRepository(LeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }
    }
}