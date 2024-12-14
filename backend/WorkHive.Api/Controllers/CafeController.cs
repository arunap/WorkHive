using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkHive.Application.Cafes.Commands.Create;
using WorkHive.Application.Cafes.Commands.Delete;
using WorkHive.Application.Cafes.Commands.Update;
using WorkHive.Application.Cafes.Queries.Dtos;
using WorkHive.Application.Cafes.Queries.Get;
using WorkHive.Application.Cafes.Queries.GetById;

namespace WorkHive.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class CafeController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET: api/Cafe/{id}
        [HttpGet("Cafe/{id}")]
        public async Task<ActionResult<CafeResult>> GetCafe(Guid id)
        {
            var query = new GetCafeByIdQuery() { CafeId = id };
            var result = await _mediator.Send(query);

            return result == null ? NotFound() : Ok(result);
        }

        // GET: api/Cafe
        [HttpGet("Cafes")]
        public async Task<ActionResult<IEnumerable<CafesByLocationResult>>> GetCafes([FromQuery] string? location)
        {
            var query = new GetCafesQuery { Location = location };
            var result = await _mediator.Send(query);

            return result == null ? NotFound() : Ok(result);
        }

        // POST: api/Cafe
        [HttpPost("Cafe")]
        public async Task<ActionResult<CafeResult>> PostCafe([FromForm] CreateCafeCommand command)
        {
            var insertedId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetCafe), new { id = insertedId }, command);
        }

        // PUT: api/Cafe/{id}
        [HttpPut("Cafe/{id}")]
        public async Task<IActionResult> PutCafe(Guid id, [FromForm] UpdateCafeCommand command)
        {
            if (id != command.Id) return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }

        // DELETE: api/Cafe/{id}
        [HttpDelete("Cafe/{id}")]
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
            var command = new DeleteCafeCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}