using AutoMapper;
using MediatR;
using Microsoft.Identity.Client.Extensions.Msal;
using VerticalSliceArch.Features.UserRoles.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.UserRoles;

public class GetUserRoleByName
{
    public class GetUserRoleByNameQuery : IRequest<GetUserRoleByNameResult>
    {
        public string RoleName { get; set; }
    }

    public class GetUserRoleByNameResult
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

    public class Handler : IRequestHandler<GetUserRoleByNameQuery, GetUserRoleByNameResult>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }
        
        public async Task<GetUserRoleByNameResult> Handle(GetUserRoleByNameQuery request,
            CancellationToken cancellationToken)
        {
            var userRole = await _serviceManager.UserRole.GetUserRoleByName(request.RoleName);

            if (userRole == null)
                throw new UserRoleNameIsNotExist(request.RoleName);

            var result = _mapper.Map<GetUserRoleByNameResult>(userRole);

            return result;

        }
    }
    
}