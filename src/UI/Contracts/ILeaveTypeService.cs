using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;
using UI.Services.Base;

namespace UI.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVM>> GetLeaveTypes();
        Task<LeaveTypeVM> GetLeaveTypeDetail(int id);
        Task<Responses<int>> CreateLeaveType(CreateLeaveTypeVM createLeaveTypeVM);
        Task<Responses<int>> UpdateLeaveType(int id, LeaveTypeVM leaveTypeVM);
        Task<Responses<int>> DeleteLeaveType(int id);
    }
}