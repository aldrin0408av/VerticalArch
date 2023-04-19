using AutoMapper;
using MediatR;
using VerticalSliceArch.Features.UserRoles.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.UserRoles;

public class GetUserRoleById
{
    public class UserRoleByIdQuery : IRequest<UserRoleByIdResult>
    {
        public int Id { get; set; }
    }
    public class UserRoleByIdResult
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public string DateAdded { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DateModified { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<UserRoleByIdQuery, UserRoleByIdResult>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<UserRoleByIdResult> Handle(UserRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var userRole = await _serviceManager.UserRole.GetUserRoleById(request.Id);

            if (userRole == null)
                throw new UserRoleNotExist(request.Id);
            var result =  _mapper.Map<UserRoleByIdResult>(userRole);
            return result;

        }
    } 
}

