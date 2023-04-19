using AutoMapper;
using VerticalSliceArch.Domain;

namespace VerticalSliceArch.Features.Games
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        { 
            CreateMap<Game, AddGameToConsole.GameResult>();
            CreateMap<Game, GetAllGamesForConsole.GameResult>();
            CreateMap<Game, UpdateGameForConsole.UpdateGameResult>();
        }
    }
}
