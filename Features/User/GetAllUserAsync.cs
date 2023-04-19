using AutoMapper;
using MediatR;
using VerticalSliceArch.Data;
using VerticalSliceArch.Features.User.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.User
{
    public abstract class GetAllUserAsync
    {
        public class GetAllUserAsyncQuery : IRequest<IEnumerable<GetAllUserAsyncResult>> { }
        public class GetAllUserAsyncResult
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool IsActive { get; set; } 
            public int DepartmentId { get; set; }
            public string DateAdded { get; set; }
            public string AddedBy { get; set; }
            public string Reason { get; set; }
        }
        
        public class Handler : IRequestHandler<GetAllUserAsyncQuery, IEnumerable<GetAllUserAsyncResult>>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }

            public async Task<IEnumerable<GetAllUserAsyncResult>> Handle(GetAllUserAsyncQuery request, CancellationToken cancellationToken)
            {
                var users = await _serviceManager.User.GetAllUsers();

                if (users == null)
                {
                    throw new NoUserFoundException();
                }

                var result = _mapper.Map<IEnumerable<GetAllUserAsyncResult>>(users);
                return result;

            }
        }
    }
}
