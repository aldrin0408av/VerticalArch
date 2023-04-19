using AutoMapper;
using MediatR;
using VerticalSliceArch.Features.Department.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.Department
{
    public class GetDepartmentById
    {
        public class GetDepartmentByIdQuery : IRequest<GetDepartmentByIdResult>
        {
            public int Id { get; set; }
        }
        public class GetDepartmentByIdResult
        {
            public int Id { get; set; }
            public string DepartmentName { get; set; }
            public string DateAdded { get; set; }
            public string AddedBy { get; set; }
            public string Reason { get; set; }
        }

        public class Handler : IRequestHandler<GetDepartmentByIdQuery, GetDepartmentByIdResult>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }

            public async Task<GetDepartmentByIdResult> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
            {
                var department = await _serviceManager.Department.GetAllDepartmentsById(request.Id);

                if (department == null)
                    throw new NoDepartmentsExists();

                var result = _mapper.Map<GetDepartmentByIdResult>(department);

                return result;
            }
        }
    }
}
