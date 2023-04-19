using AutoMapper;
using VerticalSliceArch.Data;

namespace VerticalSliceArch.Features.Department
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Departments, AddDepartment.DepartmentResult>();
            CreateMap<Departments, GetAllDepartment.DepartmentResult>();
            CreateMap<Departments, GetAllDepartmentsByStatus.GetAllDepartmentResult>();
            CreateMap<Departments, GetDepartmentById.GetDepartmentByIdResult>();
        }
    }
}
