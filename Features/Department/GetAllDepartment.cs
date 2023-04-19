using AutoMapper;
using MediatR;
using VerticalSliceArch.Features.Games;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.Department
{
    public class GetAllDepartment
    {
        public class GetDepartmentQuery : IRequest<IEnumerable<DepartmentResult>> { }

        public class DepartmentResult
        {
            public int Id { get; set; }
            public string DepartmentName { get; set; }
            public string DateAdded { get; set; }
            public string AddedBy { get; set; }
        }

        public class Handler : IRequestHandler<GetDepartmentQuery, IEnumerable<DepartmentResult>>
        {
            private readonly IServiceManager _serviceManger;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManger, IMapper mapper)
            {
                _serviceManger = serviceManger;
                _mapper = mapper;
            }

            public async Task<IEnumerable<DepartmentResult>> Handle(GetDepartmentQuery request, CancellationToken cancellationToker)
            {
                var departments = await _serviceManger.Department.GetAllDepartmentAsync();
                var result = _mapper.Map<IEnumerable<DepartmentResult>>(departments);
                return result;
            }
        }
    }
}
