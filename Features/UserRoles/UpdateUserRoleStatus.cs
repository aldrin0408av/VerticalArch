using AutoMapper;
using MediatR;
using VerticalSliceArch.Features.User.Exceptions;
using VerticalSliceArch.Features.UserRoles.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.UserRoles;

public class UpdateUserRoleStatus
{
    public class UpdateUserRoleStatusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateUserRoleStatusCommand, Unit>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserRoleStatusCommand command, CancellationToken cancellationToken)
        {
            var userRole = await _serviceManager.UserRole.GetUserRoleById(command.Id);

            if (userRole == null)
                throw new UserRoleNotExist(command.Id);

            userRole.IsActive = command.IsActive;
            await _serviceManager.SaveAsync();
            return Unit.Value;

        }
    }
}