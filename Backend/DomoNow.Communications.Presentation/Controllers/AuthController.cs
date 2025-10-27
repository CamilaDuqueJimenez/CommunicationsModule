using DomoNow.Communications.Application.UseCases.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomoNow.Communications.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(new { token });
        }
    }
}
