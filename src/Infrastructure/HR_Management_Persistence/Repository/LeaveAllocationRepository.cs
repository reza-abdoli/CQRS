using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Contracts.Persistence;
using HR_Management_Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management_Persistence.Repository
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly LeaveManagementDbContext _context;
        public LeaveAllocationRepository(LeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            return await this._context.LeaveAllocation.Include(q => q.LeaveType).ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return await this._context.LeaveAllocation.Include(q => q.LeaveType).SingleOrDefaultAsync(q => q.Id == id);
        }
    }
}