namespace Application.Common.Exceptions;

public class ForbiddenAccessException : ApplicationException
{
    public ForbiddenAccessException(string message) : base(message) { }
}