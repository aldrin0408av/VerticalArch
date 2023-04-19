using AutoMapper;
using MediatR;
using VerticalSliceArch.Data;
using VerticalSliceArch.Features.User.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.User
{
    public class GetUserById
    {
        public class GetUserByIdQuery : IRequest<GetUserByIdResult>
        {
            public int Id { get; set; }
        }
        public class GetUserByIdResult
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool IsActive { get; set; }
            public int DepartmentId { get; set; }
            public DateTime DateAdded { get; set; }
            public string AddedBy { get; set; }
            public string Reason { get; set; }
        }

        public class Hander : IRequestHandler<GetUserByIdQuery, GetUserByIdResult>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;
            public Hander(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }

            public async Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _serviceManager.User.GetUserById(request.Id);

                if (user == null)
                {
                    throw new NoUserFoundByIdException(request.Id);
                }
                var result = _mapper.Map<GetUserByIdResult>(user);
                return result;
                
            }
        }
    }
}
