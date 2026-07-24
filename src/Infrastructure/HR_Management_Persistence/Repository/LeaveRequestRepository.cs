using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Contracts.Persistence;
using HR_Management_Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management_Persistence.Repository
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly LeaveManagementDbContext _context;
        public LeaveRequestRepository(LeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task ChangeRequestStatus(LeaveRequest leaveRequest, bool? approved)
        {
            leaveRequest.Approved = approved;
            await this.Update(leaveRequest); // From parent method, which is the GenericRepository class
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            return await this._context.LeaveRequest.Include(q => q.LeaveType).ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            return await this._context.LeaveRequest.Include(q => q.LeaveType).SingleOrDefaultAsync(q => q.Id == id);
        }
    }
}