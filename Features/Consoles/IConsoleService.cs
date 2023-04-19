using VerticalSliceArch.Domain;

namespace VerticalSliceArch.Features.Consoles
{
    public interface IConsoleService
    {
        Task<IEnumerable<GameConsole>> GetAllConsolesAsync();
        Task<GameConsole> GetConsoleByIdAsync(int consoleId);
    }
}
