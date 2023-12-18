using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase {
        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: api/<LeaveAllocationController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> GetAllLeaveAllocations() {
            var result = await _mediator.Send(new GetLeaveAllocationsQuery());
            return Ok(result);
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDetailsDto>> GetLeaveAllocationDetails(int id) {
            var result = await _mediator.Send(new GetLeaveAllocationDetailsQuery(id));
            return Ok(result);
        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateLeaveAllocation([FromBody] CreateLeaveAllocationCommand request) {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(CreateLeaveAllocation), new { id = result});
        }

        // PUT api/<LeaveAllocationController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateLeaveAllocation([FromBody] UpdateLeaveAllocationCommand request) {
            await _mediator.Send(request);
            return NoContent();
        }

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id) {
            await _mediator.Send(new DeleteLeaveAllocationCommand() { Id = id});
            return NoContent();
        }
    }
}
