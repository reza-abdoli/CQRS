using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.Contracts;
using UI.Models;

namespace UI.Controllers
{
    // [Route("[controller]")]
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

        [HttpGet]
        public async Task<IActionResult> CreateLeaveTypes()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateLeaveTypes(CreateLeaveTypeVM createLeaveTypeVM)
        {
            try
            {
                var response = await _leaveTypeService.CreateLeaveType(createLeaveTypeVM);
                if (response.Success)
                {
                    return RedirectToAction("LeaveTypes");
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(createLeaveTypeVM);

        }

        [HttpGet]
        public async Task<ActionResult> EditLeaveTypes(int id)
        {
            var leaveTypeVM = await _leaveTypeService.GetLeaveTypeDetail(id);
            return View(leaveTypeVM);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLeaveTypes(int id, LeaveTypeVM leaveTypeVM)
        {
            leaveTypeVM.Id = id;
            var response = await _leaveTypeService.UpdateLeaveType(id, leaveTypeVM);
            if (response.Success)
            {
                return RedirectToAction("LeaveTypes");
            }
            ModelState.AddModelError("", response.Message);

            return View(leaveTypeVM);

        }


        [HttpGet]
        public async Task<ActionResult> DetailLeaveTypes(int id)
        {
            var leaveTypeVM = await _leaveTypeService.GetLeaveTypeDetail(id);
            return View(leaveTypeVM);

        }


        [HttpPost]
        public async Task<ActionResult> DeleteLeaveTypes(int id)
        {
            try
            {
                var response = await _leaveTypeService.DeleteLeaveType(id);
                if (response.Success)
                {
                    return RedirectToAction("LeaveTypes");
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return BadRequest();

        }

    }
}