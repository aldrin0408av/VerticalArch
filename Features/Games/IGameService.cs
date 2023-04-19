using VerticalSliceArch.Domain;

namespace VerticalSliceArch.Features.Games
{
    public interface IGameService
    {
        void AddGameToConsole(int consoleId, Game game);
        void DeleteGame(Game game);
        Task<IEnumerable<Game>> GetAllGamesAsync(int consoleId);
        Task<Game> GetGameAsync(int consoleId, int gameId);
    }
}
