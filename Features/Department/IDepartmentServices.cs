using VerticalSliceArch.Data;

namespace VerticalSliceArch.Features.Department
{
    public interface IDepartmentServices
    {
        void AddNewDepartment(Departments department);
        Task<Departments> GetDepartmentAsync(int departmentId);
        Task<IEnumerable<Departments>> GetAllDepartmentAsync();
        Task<Departments> GetDepartmentByName(string departmentName);
        Task<IEnumerable<Departments>> GetAllDepartmentsByStatus(bool status);
        Task<Departments> GetAllDepartmentsById(int Id);
    }
}
