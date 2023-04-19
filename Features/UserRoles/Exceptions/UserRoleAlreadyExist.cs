namespace VerticalSliceArch.Features.UserRoles.Exceptions;

public class UserRoleAlreadyExist : Exception
{
    public UserRoleAlreadyExist(string rolename) : base($"{rolename} is already exist"){}
}