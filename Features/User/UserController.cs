using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArch.Features.Department.Exceptions;
using VerticalSliceArch.Features.User.Exceptions;
using VerticalSliceArch.Features.UserRoles.Exceptions;
using static VerticalSliceArch.Features.User.GetAllUserAsync;
using static VerticalSliceArch.Features.User.GetUserById;
using static VerticalSliceArch.Features.User.UserByStatus;
using static VerticalSliceArch.Features.User.UserByUsername;

namespace VerticalSliceArch.Features.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddNewUser")]
        public async Task<ActionResult<QueryOrCommandResult<object>>> AddNewUser(AddNewUser.AddNewUserCommand command)
        {
            var response = new QueryOrCommandResult<object>();
            try
            {
                var result = await _mediator.Send(command);
                response.Success = true;
                response.Data = result;
                return CreatedAtRoute("GetAllUsers", result);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Success = false;
                return Conflict(response);
            }
            
        }

        [HttpGet(Name = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<GetAllUserAsyncResult>>> GetAllUser()
        {
            var response = new QueryOrCommandResult<IEnumerable<GetAllUserAsyncResult>>();
            try
            {
                var query = new GetAllUserAsyncQuery();

                var result = await _mediator.Send(query);

                response.Success = true;
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Messages.Add(ex.Message);
                return Conflict(response);
            }
        }

        [HttpGet]
        [Route("GetUsersById/{id}")]
        public async Task<ActionResult<GetUserByIdResult>> GetAllUserbyId(int id)
        {
            var response = new QueryOrCommandResult<GetUserByIdResult>();
            try
            {
                var query = new GetUserByIdQuery{

                    Id = id
                };
                
                var result = await _mediator.Send(query);
                response.Data = result;
                response.Success = true;
                return Ok(result);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Messages.Add(ex.Message);
                return Conflict(response);
            }
        }

        [HttpGet]
        [Route("GetUserByStatus")]
        public async Task<ActionResult<AllUserByStatusResult>> GetUserByStatus(bool status)
        {
            try
            {
                var query = new AllUserByStatusQuery
                {
                    IsActive = status
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (NoUserFoundException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
            
        }

        [HttpGet]
        [Route("GetUserByUserName")]
        public async Task<ActionResult<UserByUsernameResult>> GetUserByUsername(string userName)
        {
            try
            {
                var query = new UserByUsernameQuery
                {
                    Username = userName
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (NoUserFoundException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
            
        }

        [HttpPut]
        [Route("UpdateUserInformation")]
        public async Task<IActionResult> UpdateUserInformation([FromBody] UpdateUserInformation.UpdateUserInfoCommand command)
        {
            try
            {
                command.Id = command.Id;
                await _mediator.Send(command);
                return Ok($"Information of {command.FullName} is successfully updated");
            }
            catch (NoUserFoundException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }
        [HttpPut]
        [Route("UpdateUserStatus")]
        public async Task<IActionResult> UpdateUserInformation(UpdateUserStatus.UpdateUserStatusCommand command, int id)
        {
            try
            {
                command.Id = id;
                await _mediator.Send(command);
                return Ok($"User status is successfully set to {command.IsActive.ToString()}");
            }
            catch (NoUserFoundException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }
        
    }
}
