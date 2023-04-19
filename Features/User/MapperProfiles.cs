using AutoMapper;
using VerticalSliceArch.Data;

namespace VerticalSliceArch.Features.User
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Users, GetAllUserAsync.GetAllUserAsyncResult>();
            CreateMap<Users, GetUserById.GetUserByIdResult>();
            CreateMap<Users, UserByStatus.AllUserByStatusResult>();
            CreateMap<Users, UserByUsername.UserByUsernameResult>();
        }
    }
}
