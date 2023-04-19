using AutoMapper;
using MediatR;
using VerticalSliceArch.Features.Department.Exceptions;
using VerticalSliceArch.Features.User.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.User;

public class UpdateUserInformation
{
    public class UpdateUserInfoCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserInfoCommand, Unit>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateUserInfoCommand command, CancellationToken cancellationToken)
        {
            var user = await _serviceManager.User.GetUserById(command.Id);
            var department = await _serviceManager.Department.GetAllDepartmentsById(command.DepartmentId);
            
            if (user == null)
                throw new NoUserFoundException();
            
            if (department == null)
                throw new NoDepartmentsExists();
                    
            user.FullName = command.FullName;
            user.UserName = command.UserName;
            user.Password = command.Password;
            user.DepartmentId = command.DepartmentId;
            user.ModifiedBy = command.ModifiedBy;

            await _serviceManager.SaveAsync();
            return Unit.Value;

        }
    }
}