namespace VerticalSliceArch.Features.User.Exceptions
{
    public class NoUserFoundByIdException : Exception
    {
        public NoUserFoundByIdException(int id) : base($"No user found with id {id}.") { }
        
    }
}
