using AutoMapper;
using MediatR;
using VerticalSliceArch.Data;
using VerticalSliceArch.Features.Department.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.Department
{
    public class AddDepartment
    {
        public class AddDepartmentCommand : IRequest<DepartmentResult>
        {
            public string DepartmentName { get; set; }
        }

        public class DepartmentResult
        {
            public int Id { get; set; }
            public string DepartmentName { get; set; }
            public string DateAdded { get; set; }
            public string AddedBy { get; set; }
            public bool IsActive { get; set; }
            public string Reason { get; set; }
        }

        //Handler
        public class Handler : IRequestHandler<AddDepartmentCommand, DepartmentResult>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }

            public async Task<DepartmentResult> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
            {
                var department = await _serviceManager.Department.GetDepartmentByName(request.DepartmentName);

                if (department == null)
                {
                    var departments = new Departments()
                    {
                        DepartmentName = request.DepartmentName,
                        DateAdded = DateTime.Now,
                        AddedBy = "Aldrin Vega",
                        IsActive = true,
                        Reason = "Needed"
                    };
                    _serviceManager.Department.AddNewDepartment(departments);
                    await _serviceManager.SaveAsync();
                    var result = _mapper.Map<DepartmentResult>(departments);
                    
                    return result;
                }
                throw new DepartmentAlreadyExistsException(request.DepartmentName);
            }
        }
    }
}
