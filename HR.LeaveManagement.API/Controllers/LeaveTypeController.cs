using HR.LeaveManagement.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypeController : ControllerBase {
    private readonly IMediator _mediator;

    public LeaveTypeController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    // GET: api/<LeaveTypeController>
    [HttpGet]
    public async Task<List<LeaveTypeDto>> GetAllLeaveTypes() {
        var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
        return leaveTypes;
    }

    // GET api/<LeaveTypeController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveTypeDetailsDto>> GetLeaveTypeDetails(int id) {
        var leaveType = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
        return Ok(leaveType);
    }

    // POST api/<LeaveTypeController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> CreateLeaveType(CreateLeaveTypeCommand leaveTypeRequest) {
        var response = await _mediator.Send(leaveTypeRequest);
        return CreatedAtAction(nameof(CreateLeaveType), new { id = response});
    }

    // PUT api/<LeaveTypeController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateLeaveType(UpdateLeaveTypeCommand leaveTypeRequest) {
        await _mediator.Send(leaveTypeRequest);
        return NoContent();
    }

    // DELETE api/<LeaveTypeController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLeaveType(int id) {
        await _mediator.Send(new DeleteLeaveTypeCommand(id));
        return NoContent();
    }
}
