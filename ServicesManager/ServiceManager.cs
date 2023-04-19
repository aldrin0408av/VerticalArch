using VerticalSliceArch.Data;
using VerticalSliceArch.Features.Consoles;
using VerticalSliceArch.Features.Department;
using VerticalSliceArch.Features.Games;
using VerticalSliceArch.Features.User;
using VerticalSliceArch.Features.UserRoles;

namespace VerticalSliceArch.ServicesManager
{
    public class ServiceManager : IServiceManager
    {
        private readonly DataContext _context;
        private IConsoleService _consoleService;
        private IGameService _gameService;
        private IDepartmentServices _departmentServices;
        private IUserServices _userServices;
        private IUserRoleServices _userRoleServices;

        public IConsoleService Console
        {
            get
            {
                if (_consoleService == null)
                    _consoleService = new ConsoleServices(_context);
                return _consoleService;
            }
        }

        public IGameService Game
        {
            get
            {
                if (_gameService == null)
                    _gameService = new GameService(_context);
                return _gameService;
            }
        }

        public IDepartmentServices Department
        {
            get
            {
                if ( _departmentServices == null)
                    _departmentServices = new DepartmentService(_context);
                return _departmentServices;
            }
        }

        public IUserServices User
        {
            get
            {
                if (_userServices == null)
                    _userServices = new UserServices(_context);
                return _userServices;
            }
        }
        public IUserRoleServices UserRole
        {
            get
            {
                if (_userRoleServices == null)
                    _userRoleServices = new UserRoleServices(_context);
                return _userRoleServices;
            }
        }
        public ServiceManager(DataContext context)
        {
            _context = context;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
