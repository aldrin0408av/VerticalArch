using AutoMapper;
using MediatR;
using VerticalSliceArch.ServicesManager;

namespace VerticalSliceArch.Features.Consoles
{
    public class GetAllConsoles
    {
        //Input
        public class GetConsolequery : IRequest<IEnumerable<ConsoleResult>> { }

        //Output
        public class ConsoleResult
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Manufacturer { get; set; }
        }

        //Handler
        public class Handler : IRequestHandler<GetConsolequery, IEnumerable<ConsoleResult>>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }
            public async Task<IEnumerable<ConsoleResult>> Handle(GetConsolequery request, CancellationToken cancellationToken)
            {
                var consoles = await _serviceManager.Console.GetAllConsolesAsync();
                var result = _mapper.Map<IEnumerable<ConsoleResult>>(consoles);
                return result;
            }
        }

    }

}
