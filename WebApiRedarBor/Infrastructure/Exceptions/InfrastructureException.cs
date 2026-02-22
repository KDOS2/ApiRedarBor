namespace Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message) : base(message) { }

        public InfrastructureException(string message, Exception inner): base(message, inner) { }
    }
}
