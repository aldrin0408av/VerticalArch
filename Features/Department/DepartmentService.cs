using Microsoft.EntityFrameworkCore;
using VerticalSliceArch.Data;

namespace VerticalSliceArch.Features.Department
{
    public class DepartmentService : IDepartmentServices
    {
        private readonly DataContext _context;
        public DepartmentService(DataContext context)
        { 
            _context = context;
        }

        public void AddNewDepartment(Departments department)
        {
            _context.Add(department);
        }
        public async Task<Departments> GetDepartmentAsync(int departmentId)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Id == departmentId);
        }
        public async Task<IEnumerable<Departments>> GetAllDepartmentAsync()
        {
            return await _context.Departments.ToListAsync();
        }
        public async Task<Departments> GetDepartmentByName(string departmentName)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentName == departmentName);
        }
        public async Task<IEnumerable<Departments>> GetAllDepartmentsByStatus(bool status)
        {
            return await _context.Departments.Where(x => x.IsActive == status).ToListAsync();
        }
        public async Task<Departments> GetAllDepartmentsById(int Id)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Id == Id);
        }

    }
}
