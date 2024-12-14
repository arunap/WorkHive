using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkHive.Application.Employees.Commands.Create;
using WorkHive.Application.Employees.Commands.Delete;
using WorkHive.Application.Employees.Commands.Update;
using WorkHive.Application.Employees.Queries.Dtos;
using WorkHive.Application.Employees.Queries.Get;
using WorkHive.Application.Employees.Queries.GetById;

namespace WorkHive.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class EmployeeController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET: api/Employee/{id}
        [HttpGet("Employee/{id}")]
        public async Task<ActionResult<EmployeeResult>> GetEmployee(string id)
        {
            var query = new GetEmployeeByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            return result == null ? NotFound() : Ok(result);
        }

        // GET: api/Employees
        [HttpGet("Employees")]
        public async Task<ActionResult<IEnumerable<EmployeesByCafeNameResult>>> GetEmployees(string? cafe)
        {
            var query = new GetEmployeesQuery { CafeName = cafe };
            var result = await _mediator.Send(query);

            return result == null ? NotFound() : Ok(result);
        }

        // POST: api/Employee
        [HttpPost("Employee")]
        public async Task<ActionResult<EmployeeResult>> PostEmployee(CreateEmployeeCommand command)
        {
            var insertedId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetEmployee), new { id = insertedId }, command);
        }

        // PUT: api/Employee/{id}
        [HttpPut("Employee/{id}")]
        public async Task<IActionResult> PutEmployee(string id, UpdateEmployeeCommand command)
        {
            if (id != command.EmployeeId) return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }

        // DELETE: api/Employee/{id}
        [HttpDelete("Employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}