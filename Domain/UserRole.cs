namespace VerticalSliceArch.Data
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string Reason { get; set; }
    }
}
