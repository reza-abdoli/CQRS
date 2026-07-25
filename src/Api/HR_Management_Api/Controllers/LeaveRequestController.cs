using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Dtos;
using HR_Management_Application.Dtos.LeaveRequest;
using HR_Management_Application.Dtos.LeaveType;
using HR_Management_Application.Features.LeaveRequest.Requests;
using HR_Management_Application.Features.LeaveRequest.Requests.Commands;
using HR_Management_Application.Features.LeaveRequest.Requests.Queries;
using HR_Management_Application.Features.LeaveType.Requests;
using HR_Management_Application.Features.LeaveType.Requests.Commands;
using HR_Management_Application.Features.LeaveType.Requests.Queries;
using HR_Management_Domain;
using HR_Management_Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestController : ControllerBase
    {
        public readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveType>>> GetLeaveRequests()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveTypes);
        }

        // GET: api/LeaveTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveType>> GetLeaveRequest(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
            return Ok(leaveType);

        }

        // POST: api/LeaveTypes
        [HttpPost]
        public async Task<ActionResult<LeaveType>> PostLeaveRequest([FromBody] CreateLeaveRequestDto createLeaveRequestDto)
        {
            var response = await _mediator.Send(new CreateLeaveRequestCommand { CreateLeaveRequestDto = createLeaveRequestDto });

            return response.Success ? Ok(response) : BadRequest(response);
        }

        // PUT: api/LeaveTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveRequest(int id, UpdateLeaveRequestDto updateLeaveRequestDto)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand { UpdateLeaveRequestDto = updateLeaveRequestDto, Id = id });

            return NoContent();
        }



        // DELETE: api/LeaveTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommand { Id = id });

            return NoContent();
        }

        [HttpPut("changeapproval/{id}")]
        public async Task<IActionResult> ChangeApproval(int id,ChangeLeaveRequestStateDto changeLeaveRequestStateDto)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand { Id = id, ChangeLeaveRequestStateDto = changeLeaveRequestStateDto });

            return NoContent();
        }

    }
}