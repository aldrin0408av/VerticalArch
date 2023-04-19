using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSliceArch.Features.Consoles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ConsolesController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllConsoles.ConsoleResult>>> GetConsoleAsync()
        {
            var result = await _mediator.Send(new GetAllConsoles.GetConsolequery());

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
