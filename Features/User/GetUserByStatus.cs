using System.Runtime.CompilerServices;
using AutoMapper;
using MediatR;
using VerticalSliceArch.Data;
using VerticalSliceArch.Features.Consoles;
using VerticalSliceArch.Features.Department;
using VerticalSliceArch.Features.User.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.User;

public class UserByStatus
{
    public class AllUserByStatusQuery : IRequest<IEnumerable<AllUserByStatusResult>>
    {
        public bool IsActive { get; set; }
    }

    public class AllUserByStatusResult
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public string DateAdded { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Reason { get; set; }
        
    }

    public class Handler : IRequestHandler<AllUserByStatusQuery, IEnumerable<AllUserByStatusResult>>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AllUserByStatusResult>> Handle(AllUserByStatusQuery request, CancellationToken cancellationToken)
        {
             var user = await _serviceManager.User.GetUserByStatus(request.IsActive);
             if (!user.Any())
                throw new NoUserFoundException();
             var results = _mapper.Map<IEnumerable<AllUserByStatusResult>>(user).ToList();
             
             foreach (var result in results)
             {
                 var department = await _serviceManager.Department.GetAllDepartmentsById(result.DepartmentId);
                 result.DepartmentName = department.DepartmentName;
             }
             return results;
        }
    }
}