namespace VerticalSliceArch.Data
{
    public class Departments
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
        public string Reason { get; set; }
    }
}
