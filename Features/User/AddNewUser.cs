using AutoMapper;
using MediatR;
using Microsoft.Identity.Client;
using VerticalSliceArch.Data;
using VerticalSliceArch.Features.Department.Exceptions;
using VerticalSliceArch.Features.User.Exceptions;
using VerticalSliceArch.Features.UserRoles.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.User
{
    public class AddNewUser
    {
        public class AddNewUserCommand : IRequest<Unit>
        {
            public string FullName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            
            public int UserRoleId { get; set; }
            public int DepartmentId { get; set; }
            public DateTime DateAdded { get; set; } = DateTime.Now;
        }
       
        public class Handler : IRequestHandler<AddNewUserCommand, Unit>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(AddNewUserCommand command, CancellationToken cancellationToken)
            {
                var user = await _serviceManager.User.GetUserByName(command.UserName);

                var department = await _serviceManager.Department.GetAllDepartmentsById(command.DepartmentId);
                var userRole = await _serviceManager.UserRole.GetUserRoleById(command.UserRoleId);

                if (user != null) throw new UserAlreadyExistsException(command.UserName);
                if(department == null)
                    throw new DepartmentIsNotExists(command.DepartmentId);
                if (userRole == null)
                    throw new UserRoleNotExist(command.UserRoleId);
                var users = new Users()
                {
                    FullName = command.FullName,
                    UserName = command.UserName,
                    Password = command.Password,
                    IsActive = true,
                    UserRoleId = command.UserRoleId,
                    DepartmentId = command.DepartmentId,
                    DateAdded = command.DateAdded,
                    AddedBy = "Aldrin Vega",
                    Reason = "Needed"
                };
                _serviceManager.User.AddUser(users);
                await _serviceManager.SaveAsync();
                return Unit.Value;

            }
        }
    }
}
