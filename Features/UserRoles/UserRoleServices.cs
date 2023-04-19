using Microsoft.EntityFrameworkCore;
using VerticalSliceArch.Data;

namespace VerticalSliceArch.Features.UserRoles;

public class UserRoleServices : IUserRoleServices
{
    private readonly DataContext _context;

    public  UserRoleServices(DataContext context)
    {
        _context = context;
    }

    public void AddNewRoles(UserRole userRole)
    {
        _context.UserRoles.Add(userRole);
    }

    public async Task<UserRole> GetUserRoleById(int id)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<UserRole>> GetAllUserRole()
    {
        return await _context.UserRoles.ToListAsync();
    }

    public async Task<IEnumerable<UserRole>> GetUserRolesByStatus(bool status)
    {
        return await _context.UserRoles.Where(x => x.IsActive == status).ToListAsync();
    }

    public async Task<UserRole> GetUserRoleByName(string userRoleName)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(x => x.RoleName == userRoleName);
    }
    
}