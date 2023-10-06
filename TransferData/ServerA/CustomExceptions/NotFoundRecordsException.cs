using System;
namespace ServerA.CustomExceptions
{
	public class NotFoundRecordsException : Exception
    {
        public NotFoundRecordsException()
        {
        }

        public NotFoundRecordsException(string message)
            : base(message)
        {
        }

        public NotFoundRecordsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

