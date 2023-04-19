namespace VerticalSliceArch.Features.Department.Exceptions
{
    public class NoDepartmentsExists : Exception
    {
        public NoDepartmentsExists() : base($"No Departments Found") {}
    }
}
