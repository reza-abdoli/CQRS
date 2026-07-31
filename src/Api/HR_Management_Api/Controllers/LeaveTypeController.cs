using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Dtos;
using HR_Management_Application.Dtos.LeaveType;
using HR_Management_Application.Features.LeaveType.Requests;
using HR_Management_Application.Features.LeaveType.Requests.Commands;
using HR_Management_Application.Features.LeaveType.Requests.Queries;
using HR_Management_Application.Responses;
using HR_Management_Domain;
using HR_Management_Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTypeController : ControllerBase
    {
        public readonly IMediator _mediator;

        public LeaveTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/LeaveTypes
        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> GetLeaveTypes()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
        }

        // GET: api/LeaveTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDto>> GetLeaveType(int id)
        {
            var leaveTypeDto = await _mediator.Send(new GetLeaveTypeDetailRequest { Id = id });
            return Ok(leaveTypeDto);

        }

        // POST: api/LeaveTypes
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> PostLeaveType([FromBody] CreateLeaveTypeDto createLeaveTypeDto)
        {
            var response = await _mediator.Send(new CreateLeaveTypeCommand { CreateLeaveTypeDto = createLeaveTypeDto });

            return response.Success ? Ok(response) : BadRequest(response);
        }

        // PUT: api/LeaveTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveType(int id, LeaveTypeDto leaveTypeDto)
        {
            await _mediator.Send(new UpdateLeaveTypeCommand { LeaveTypeDto = new LeaveTypeDto { Id = id, Name = leaveTypeDto.Name, DefaultDay = leaveTypeDto.DefaultDay } });

            return Ok();
        }



        // DELETE: api/LeaveTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveType(int id)
        {
            await _mediator.Send(new DeleteLeaveTypeCommand { Id = id });

            return Ok();
        }

    }
}