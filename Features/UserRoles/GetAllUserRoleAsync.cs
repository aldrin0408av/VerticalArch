using AutoMapper;
using MediatR;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.UserRoles;

public class AllUserRoleAsync
{
    public class AllUserRoleAsyncQuery : IRequest<IEnumerable<AllUserRoleAsyncResult>>  { }

    public class AllUserRoleAsyncResult
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

    public class Handler : IRequestHandler<AllUserRoleAsyncQuery, IEnumerable<AllUserRoleAsyncResult>>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AllUserRoleAsyncResult>> Handle(AllUserRoleAsyncQuery request,
            CancellationToken cancellationToken)
        {
            var departments = await _serviceManager.UserRole.GetAllUserRole();
            var result = _mapper.Map<IEnumerable<AllUserRoleAsyncResult>>(departments);

            return result;
        }
    }
}