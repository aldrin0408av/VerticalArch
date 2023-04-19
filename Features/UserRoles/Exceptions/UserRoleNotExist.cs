namespace VerticalSliceArch.Features.UserRoles.Exceptions;

public class UserRoleNotExist : Exception
{
    public UserRoleNotExist(int roleId) : base($" User Role with id {roleId} is not exists"){}
}