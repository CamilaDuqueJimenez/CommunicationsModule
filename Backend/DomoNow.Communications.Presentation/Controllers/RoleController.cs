using DomoNow.Communications.Application.UseCases.Role.Commands;
using DomoNow.Communications.Application.UseCases.Role.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomoNow.Communications.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }
    }
}
