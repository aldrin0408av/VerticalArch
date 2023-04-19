using AutoMapper;
using MediatR;
using VerticalSliceArch.Features.Department.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.Department
{
    public class UpdateDepartmentStatus
    {
        public class UpdateDepartmentStatusCommand : IRequest<Unit>
        {
            public int Id { get; set; }
            public bool IsActive { get; set; }
        }

        public class Handler : IRequestHandler<UpdateDepartmentStatusCommand, Unit>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }

            public async Task<Unit> Handle (UpdateDepartmentStatusCommand command, CancellationToken cancellationToken)
            {
                var department = await _serviceManager.Department.GetAllDepartmentsById(command.Id);

                if (department == null)
                    throw new DepartmentIsNotExists(command.Id);

                department.IsActive = command.IsActive;

                await _serviceManager.SaveAsync();

                return Unit.Value;
            }
        }
    }
}
