using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using VerticalSliceArch.Features.User.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.UserRoles;

public class GetAllUserRoleByStatus
{
    public class AllUserRoleByStatusQuery : IRequest<IEnumerable<AllUserRoleByStatusResult>>
    {
        public bool IsActive { get; set; }
    }

    public class AllUserRoleByStatusResult
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<AllUserRoleByStatusQuery, IEnumerable<AllUserRoleByStatusResult>>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AllUserRoleByStatusResult>> Handle(AllUserRoleByStatusQuery request,
            CancellationToken cancellationToken)
        {
            var query = await _serviceManager.UserRole.GetUserRolesByStatus(request.IsActive);
            if (query == null)
                throw new NoUserFoundException();

            var result = _mapper.Map<IEnumerable<AllUserRoleByStatusResult>>(query);
            return result;
        }
    }
}