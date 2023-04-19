using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArch.Features.Games.Exceptions;
using static VerticalSliceArch.Features.Games.AddGameToConsole;
using static VerticalSliceArch.Features.Games.GetAllGamesForConsole;

namespace VerticalSliceArch.Features.Games
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Name = "GetGamesForConsole")]
        public async Task<ActionResult<IEnumerable<GetAllGamesForConsole.GameResult>>> GetGamesForConsole(int consoleId)
        {
            try
            {
                var query = new GetGamesQuery
                {
                    ConsoleId = consoleId
                };

                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (NoConsoleExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddGame(AddGameToConsole.AddGameCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return CreatedAtRoute("GetGamesForConsole", new { consoleId = result.ConsoleId }, result);
            }
            catch (NoConsoleExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpateGameFroConsole(int consoleId, UpdateGameForConsole.UpdateGameCommand command)
        {
            try
            {
                command.ConsoleId = consoleId;
                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoGameExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message,
                    ex.ConsoleId,
                    ex.GameId
                });
            }
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveGameFromConsole(int consoleId, RemoveGameFromConsole.RemoveGameCommand command)
        {
            try
            {
                command.ConsoleId = consoleId;
                await _mediator.Send(command);
                return NoContent();
            }
            catch (NoConsoleExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
            catch (NoGameExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message,
                    ex.ConsoleId,
                    ex.GameId

                });
            }
        }
    }
}
