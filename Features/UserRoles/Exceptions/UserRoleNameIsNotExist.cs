namespace VerticalSliceArch.Features.UserRoles.Exceptions;

public class UserRoleNameIsNotExist : Exception
{
    public UserRoleNameIsNotExist(string roleName) : base($"{roleName} is not exist") { }
}