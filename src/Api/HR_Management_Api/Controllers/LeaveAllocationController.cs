using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Dtos;
using HR_Management_Application.Dtos.LeaveAllocation;
using HR_Management_Application.Dtos.LeaveType;

using HR_Management_Domain;
using HR_Management_Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HR_Management_Application.Features.LeaveAllocation.Requests;
using HR_Management_Application.Features.LeaveAllocation.Requests.Queries;
using HR_Management_Application.Features.LeaveAllocation.Requests.Commands;

namespace HR_Management_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveAllocationController : ControllerBase
    {
        public readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveType>>> GetLeaveAllocations()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveAllocationListRequest());
            return Ok(leaveTypes);
        }

        // GET: api/LeaveTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveType>> GetLeaveAllocation(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveAllocationDetailRequest { Id = id });
            return Ok(leaveType);

        }

        // POST: api/LeaveTypes
        [HttpPost]
        public async Task<ActionResult<LeaveType>> PostLeaveAllocation([FromBody] CreateLeaveAllocationDto createLeaveAllocationDto)
        {
            var response = await _mediator.Send(new CreateLeaveAllocationCommand { CreateLeaveAllocationDto = createLeaveAllocationDto });

            return Ok(response);
        }

        // PUT: api/LeaveTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveAllocation(UpdateLeaveAllocationDto updateLeaveAllocationDto)
        {
            await _mediator.Send(new UpdateLeaveAllocationCommand { UpdateLeaveAllocationDto = updateLeaveAllocationDto });

            return NoContent();
        }



        // DELETE: api/LeaveTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveAllocation(int id)
        {
            await _mediator.Send(new DeleteLeaveAllocationCommand { Id = id });

            return NoContent();
        }

    }
}