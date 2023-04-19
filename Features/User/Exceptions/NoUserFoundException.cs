namespace VerticalSliceArch.Features.User.Exceptions
{
    public class NoUserFoundException : Exception
    {
        public NoUserFoundException() : base($"No Users has been found.") { }
    }
}

