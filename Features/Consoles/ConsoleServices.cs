using Microsoft.EntityFrameworkCore;
using VerticalSliceArch.Data;
using VerticalSliceArch.Domain;

namespace VerticalSliceArch.Features.Consoles
{
    public class ConsoleServices : IConsoleService
    {
        private readonly DataContext _context;

        public ConsoleServices(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameConsole>> GetAllConsolesAsync()
        {
            return await _context.Consoles.OrderBy(x => x.Id).ToListAsync();
        }
        public async Task<GameConsole> GetConsoleByIdAsync(int consoleId)
        {
            return await _context.Consoles
                .FirstOrDefaultAsync(x => x.Id == consoleId);

        }
    }
}
