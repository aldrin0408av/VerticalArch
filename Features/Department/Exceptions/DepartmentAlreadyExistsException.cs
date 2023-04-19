namespace VerticalSliceArch.Features.Department.Exceptions
{
    public class DepartmentAlreadyExistsException : Exception
    {
        public DepartmentAlreadyExistsException(string departmentName) : base($"{departmentName} is already exists") { }
    }
}
