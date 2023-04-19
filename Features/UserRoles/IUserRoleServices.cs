using VerticalSliceArch.Data;

namespace VerticalSliceArch.Features.UserRoles;

public interface IUserRoleServices
{
    Task<UserRole> GetUserRoleByName(string userRoleName);
    Task<IEnumerable<UserRole>> GetUserRolesByStatus(bool status);
    Task<IEnumerable<UserRole>> GetAllUserRole();
    Task<UserRole> GetUserRoleById(int id);
    void AddNewRoles(UserRole userRole);
}