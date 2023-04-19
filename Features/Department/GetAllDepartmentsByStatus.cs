using AutoMapper;
using MediatR;
using VerticalSliceArch.Features.Department.Exceptions;
using VerticalSliceArch.ServicesManager;
using static VerticalSliceArch.Features.Department.GetAllDepartmentsByStatus;

namespace VerticalSliceArch.Features.Department
{
    public class GetAllDepartmentsByStatus 
    {
        public class GetAllDepartmentQuery : IRequest<IEnumerable<GetAllDepartmentResult>>
        {
            public bool IsActive { get; set; }
        }
        public class GetAllDepartmentResult
        {
            public int Id { get; set; }
            public string DepartmentName { get; set; }
            public string DateAdded { get; set; }
            public string AddedBy { get; set; }
            public string Reason { get; set; }
        }

    }
  

    public class Handler : IRequestHandler<GetAllDepartmentQuery, IEnumerable<GetAllDepartmentResult>>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllDepartmentResult>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var departments = await _serviceManager.Department.GetAllDepartmentsByStatus(request.IsActive);

            if (departments == null)
                throw new NoDepartmentsExists();

            var result = _mapper.Map<IEnumerable<GetAllDepartmentResult>>(departments);

            return result;

        }
    }
}
