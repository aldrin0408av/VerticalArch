using VerticalSliceArch.Features.Consoles;
using VerticalSliceArch.Features.Department;
using VerticalSliceArch.Features.Games;
using VerticalSliceArch.Features.User;
using VerticalSliceArch.Features.UserRoles;

namespace VerticalSliceArch.ServicesManager
{
    public interface IServiceManager
    {
        IConsoleService Console { get; }
        IGameService Game { get; }
        IDepartmentServices Department { get; }
        IUserServices User { get; }
        IUserRoleServices UserRole { get; }
        Task SaveAsync();
    }
}
