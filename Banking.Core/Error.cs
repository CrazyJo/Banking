namespace Banking.Core
{
    public class Error
    {
        public string Message { get; private set; }
        public string Property { get; private set; }

        public Error()
        {
        }

        public Error(string message) : this(string.Empty, message)
        {
        }

        public Error(string property, string message)
        {
            Property = property;
            Message = message;
        }
    }
}
