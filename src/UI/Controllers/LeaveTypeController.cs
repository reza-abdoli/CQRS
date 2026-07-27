using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.Contracts;

namespace UI.Controllers
{
    [Route("[controller]")]
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> LeaveTypes()
        {
            var leaveTypesVM = await _leaveTypeService.GetLeaveTypes();
            return View(leaveTypesVM);
        }

        // public IActionResult GetDetails(int id)
        // {
        //     return View("Error!");
        // }
    }
}