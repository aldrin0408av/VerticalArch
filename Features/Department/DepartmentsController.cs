using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArch.Features.Department.Exceptions;
using static VerticalSliceArch.Features.Department.GetAllDepartment;
using static VerticalSliceArch.Features.Department.GetAllDepartmentsByStatus;
using static VerticalSliceArch.Features.Department.GetDepartmentById;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VerticalSliceArch.Features.Department
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Name = "GetAllDepartments")]
        public async Task<ActionResult<IEnumerable<GetAllDepartment.DepartmentResult>>> GetAllDepartments()
        {
            try
            {
                var query = new GetDepartmentQuery();

                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddDepartment(AddDepartment.AddDepartmentCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtRoute("GetAllDepartments", result);
            }
            catch (DepartmentAlreadyExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message,
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeparmtent(int departmentId, UpdateDepartment.UpdateDepartmentCommand command)
        {
            try
            {
                command.Id = departmentId;

                await _mediator.Send(command);

                return Ok($"Department ID {command.Id} is successfully updated to {command.DepartmentName}");
            }
            catch (DepartmentIsNotExists ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
            catch (DepartmentAlreadyExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }
        [HttpPut]
        [Route("UpdateDepartmentStatus")]
        public async Task<IActionResult> UpdateDeprartmentStatus(int id, UpdateDepartmentStatus.UpdateDepartmentStatusCommand command)
        {
            try
            {
                command.Id = id;
                var result = await _mediator.Send(command);

                return Ok("Department status is successfuly updated");
            }
            catch (DepartmentIsNotExists ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetAllDepartmentByStatus/{status}")]
        public async Task<ActionResult<IEnumerable<GetAllDepartmentResult>>> GetAllDepartmentsByStatus(bool status)
        {
            try
            {

                var query = new GetAllDepartmentQuery
                {
                    IsActive = status
                };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (NoDepartmentsExists ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }
        [HttpGet]
        [Route("GetDepartmentByStatus/{id}")]
        public async Task<ActionResult<GetDepartmentByIdQuery>> GetAllDepartmentsById(int id)
        {
            try
            {

                var query = new GetDepartmentByIdQuery
                {
                    Id = id
                };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (NoDepartmentsExists ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }
    }

}

