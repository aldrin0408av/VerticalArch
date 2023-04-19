using AutoMapper;
using VerticalSliceArch.Domain;
using VerticalSliceArch.Features.Games;

namespace VerticalSliceArch.Features.Consoles
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<GameConsole, GetAllConsoles.ConsoleResult>();
        }
    }
}
