using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArch.Data;
using VerticalSliceArch.Features.User;
using VerticalSliceArch.Features.UserRoles.Exceptions;
using static VerticalSliceArch.Features.UserRoles.AddNewUserRole;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VerticalSliceArch.Features.UserRoles
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult> AddNewUserRole(AddNewRoleCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok($"UserRole {command.RoleName} is added");
            }
            catch (UserRoleAlreadyExist ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }

        [HttpGet("GetAllUserRoleAsync")]
        public async Task<ActionResult<IEnumerable<AllUserRoleAsync.AllUserRoleAsyncResult>>> GetAllUserRole()
        {
            try
            {
                var query = new AllUserRoleAsync.AllUserRoleAsyncQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Conflict(new
                {
                    e.Message
                });
            }
        }

        [HttpGet("GetUserRoleById/{id}")]
        public async Task<ActionResult<AllUserRoleAsync.AllUserRoleAsyncResult>> GetUserRoleById(int id)
        {
            try
            {
                var query = new GetUserRoleById.UserRoleByIdQuery
                {
                    Id = id
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Conflict(new
                {
                    e.Message
                });
            }
        }

        [HttpGet("GetUserROleByName/{roleName}")]
        public async Task<ActionResult<GetUserRoleByName.GetUserRoleByNameResult>> GetUserRoleByName(string roleName)
        {
            try
            {
                var query = new GetUserRoleByName.GetUserRoleByNameQuery
                {
                    RoleName = roleName
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Conflict(new
                {
                    e.Message
                });
            }
        }

        [HttpGet("GetAllUserRoleByStatus/{status}")]
        public async Task<ActionResult<IEnumerable<GetAllUserRoleByStatus.AllUserRoleByStatusResult>>>
            GetAllUserRoleByStatus(bool status)
        {
            try
            {
                var query = new UserByStatus.AllUserByStatusQuery
                {
                    IsActive = status
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Conflict(new
                {
                    e.Message
                });
            }
        }

        [HttpPut("UpdateUserRoleStatus")]
        public async Task<ActionResult> UpdateUserRoleStatus(UpdateUserRoleStatus.UpdateUserRoleStatusCommand command)
        {
            try
            {
                command.Id = command.Id;
                await _mediator.Send(command);
                return Ok($"User Role successfully set to {command.IsActive}");
            }
            catch (Exception e)
            {
                return Conflict(new
                {
                    e.Message
                });
            }
        }

    }
}
