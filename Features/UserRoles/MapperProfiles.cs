using AutoMapper;
using VerticalSliceArch.Data;

namespace VerticalSliceArch.Features.UserRoles;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<UserRole, AllUserRoleAsync.AllUserRoleAsyncResult>();
        CreateMap<UserRole, GetUserRoleById.UserRoleByIdResult>();
        CreateMap<UserRole, GetUserRoleByName.GetUserRoleByNameResult>();
    }
}