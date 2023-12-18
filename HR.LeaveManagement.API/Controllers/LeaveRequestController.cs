using Azure.Core;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetAllLeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestController : ControllerBase {
    private readonly IMediator _mediator;

    public LeaveRequestController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    // GET: api/<LeaveRequestController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LeaveRequestListDto>>> GetAllLeaveRequests() {
        var result = await _mediator.Send(new GetLeaveRequestDetailsQuery());
        return Ok(result);
    }

    // GET api/<LeaveRequestController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveRequestDetailsDto>> GetLeaveRequestDetails(int id) {
        var result = await _mediator.Send(new GetLeaveRequestDetailsQuery() { Id = id });
        return Ok(result);
    }

    // POST api/<LeaveRequestController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateLeaveRequest([FromBody] CreateLeaveRequestCommand request) {
        await _mediator.Send(request);
        return NoContent();
    }

    // PUT api/<LeaveRequestController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateLeaveRequest([FromBody] UpdateLeaveRequestCommand request) {
        await _mediator.Send(request);
        return NoContent();
    }

    // PUT api/<LeaveRequestController>/5
    [HttpPut]
    [Route("approve")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CancelLeaveRequest([FromBody] CancelLeaveRequestCommand request) {
        await _mediator.Send(request);
        return NoContent();
    }

    // PUT api/<LeaveRequestController>/5
    [HttpPut]
    [Route("cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ApproveLeaveRequest([FromBody] ChangeLeaveRequestApprovalCommand request) {
        await _mediator.Send(request);
        return NoContent();
    }

    // DELETE api/<LeaveRequestController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteLeaveRequest(int id) {
        await _mediator.Send(new DeleteLeaveRequestCommand() { Id = id });
        return NoContent();
    }
}
