namespace Backend.Exceptions;

[Serializable]
internal class CantGetFileTypeException : Exception
{
    public CantGetFileTypeException()
    {
    }

    public CantGetFileTypeException(string? message) : base(message)
    {
    }

    public CantGetFileTypeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}