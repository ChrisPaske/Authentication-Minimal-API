namespace Authentication_Minimal_API.Core;

public class RepositoryOperationException : Exception
{
    public RepositoryOperationException()
    {
    }

    public RepositoryOperationException(string message) : base(message)
    {
    }

    public RepositoryOperationException(string message, Exception inner) : base(message, inner)
    {
    }
}