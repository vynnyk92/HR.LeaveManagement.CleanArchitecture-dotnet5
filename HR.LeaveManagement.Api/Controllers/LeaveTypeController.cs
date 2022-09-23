using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("", Name = "GetAll")]
        public async Task<IActionResult> Get()
        {
            var getAllQuery = new GetLeaveTypeListRequest();
            var allTypes = await _mediator.Send(getAllQuery);
            return Ok(allTypes);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var getLeaveTypeDetailRequest = new GetLeaveTypeDetailRequest(id);
            var getLeaveType = await _mediator.Send(getLeaveTypeDetailRequest);
            return Ok(getLeaveType);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeaveTypeDto createLeaveTypeDto)
        {
            var createLeaveTypeCommand = new CreateLeaveTypeCommand(createLeaveTypeDto);
            var leaveTypeId = await _mediator.Send(createLeaveTypeCommand);
            return CreatedAtRoute("Get", routeValues: new {id = leaveTypeId}, value: leaveTypeId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] LeaveTypeDto leaveTypeDto)
        {
            var updateLeaveTypeCommand = new UpdateLeaveTypeCommand(leaveTypeDto);
            await _mediator.Send(updateLeaveTypeCommand);
            return CreatedAtRoute("Get", routeValues: new { id = id }, value: leaveTypeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleteLeaveTypeCommand = new DeleteLeaveTypeCommand(){Id = id };
            await _mediator.Send(deleteLeaveTypeCommand);
            return NoContent();
        }
    }
}
