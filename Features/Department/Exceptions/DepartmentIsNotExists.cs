namespace VerticalSliceArch.Features.Department.Exceptions
{
    public class DepartmentIsNotExists : Exception
    {
        public DepartmentIsNotExists(int id) : base($"Department Id {id} is not exists") { }
    }
}
