using AutoMapper;
using MediatR;
using VerticalSliceArch.Features.Department;
using VerticalSliceArch.Features.User.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.User;

public class UserByUsername
{
    public class UserByUsernameQuery : IRequest<UserByUsernameResult>
    {
        public string Username { get; set; }
    }

    public class UserByUsernameResult
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string DepartmentName { get; set; }
        public string DateAdded { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<UserByUsernameQuery, UserByUsernameResult>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<UserByUsernameResult>
            Handle(UserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _serviceManager.User.GetuserByUsername(request.Username);
            if (user == null)
                throw new NoUserFoundException();
            var results = _mapper.Map<UserByUsernameResult>(user);
            var department = await _serviceManager.Department.GetAllDepartmentsById(user.DepartmentId);
            results.DepartmentName = department.DepartmentName;

            return results;
        }
    }
}