using AutoMapper;
using MediatR;
using VerticalSliceArch.Features.Department.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.Department
{
    public class UpdateDepartment
    {
        public class UpdateDepartmentCommand : IRequest<Unit>
        {
            public int Id { get; set; }
            public string DepartmentName { get; set; }
        }

        public class UpdateDepartmentResult
        {
            public int Id { get; set; }
            public string DepartmentName { get; set; }
            public DateTime DateAdded { get; set; }
            public string AddedBy { get; set; }
        }


        public class Handler : IRequestHandler<UpdateDepartmentCommand, Unit>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
            {
                var department = await _serviceManager.Department.GetDepartmentAsync(request.Id);

                var departmentName = await _serviceManager.Department.GetDepartmentByName(request.DepartmentName);

                if (departmentName != null)
                    throw new DepartmentAlreadyExistsException(request.DepartmentName);

                if (department == null)
                    throw new DepartmentIsNotExists(request.Id);


                department.DepartmentName = request.DepartmentName;
                department.DateAdded = DateTime.Now;
                department.AddedBy = "Aldrin Vega";
                await _serviceManager.SaveAsync();
                return Unit.Value;



            }
        }
    }
}
