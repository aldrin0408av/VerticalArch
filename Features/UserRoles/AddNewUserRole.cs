using AutoMapper;
using MediatR;
using VerticalSliceArch.Data;
using VerticalSliceArch.Features.UserRoles.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.UserRoles;

public class AddNewUserRole
{
    public class AddNewRoleCommand : IRequest<Unit>
    {
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
        public string Reason { get; set; }
    }

    
    public class Handler : IRequestHandler<AddNewRoleCommand, Unit>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddNewRoleCommand command, CancellationToken cancellationToken)
        {
            var role = await _serviceManager.UserRole.GetUserRoleByName(command.RoleName);

            if (role == null)
            {
                var roles = new UserRole()
                {
                    RoleName = command.RoleName,
                    IsActive = command.IsActive,
                    DateAdded = DateTime.Now,
                    AddedBy = command.AddedBy,
                    Reason = command.Reason
                };
                _serviceManager.UserRole.AddNewRoles(roles);
                await _serviceManager.SaveAsync();
            }
            throw new UserRoleAlreadyExist(command.RoleName);
            
        }
    }
}