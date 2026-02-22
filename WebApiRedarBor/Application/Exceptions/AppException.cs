namespace Application.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message) { }
        public AppException(string message, Exception inner) : base(message, inner) { }
    }

    public class AppNotFoundException : Exception
    {
        public AppNotFoundException(string message) : base(message) { }
        public AppNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
