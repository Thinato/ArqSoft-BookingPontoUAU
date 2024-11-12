namespace Application.Errors
{
    public class UpdateException : Exception
    {
        public UpdateException(string message) : base(message) { }
    }
}