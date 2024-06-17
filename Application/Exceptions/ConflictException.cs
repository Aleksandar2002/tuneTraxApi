namespace Application.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string reason) : base(reason)
    {
    }

}
