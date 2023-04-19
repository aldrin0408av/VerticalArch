using AutoMapper;
using MediatR;
using VerticalSliceArch.Features.User.Exceptions;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.User;

public class UpdateUserStatus
{
    public class UpdateUserStatusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserStatusCommand, Unit>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateUserStatusCommand command, CancellationToken cancellationToken)
        {
            var user = await _serviceManager.User.GetUserById(command.Id);
            if (user == null)
                throw new NoUserFoundException();
            user.IsActive = command.IsActive;
            await _serviceManager.SaveAsync();
            return Unit.Value;
        }
    }
}