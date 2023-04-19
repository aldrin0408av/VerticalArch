namespace VerticalSliceArch.Features.User.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string userName) : base($"Username: {userName} already exist.") { }
    }
}
