using Microsoft.AspNetCore.Identity;

namespace VerticalSliceArch.Data
{
    public class Users
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        
        public UserRole UserRole { get; set; }
        public int UserRoleId { get; set; }

        public Departments Department { get; set; }
        public int DepartmentId { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Reason { get; set; }
    }
}
