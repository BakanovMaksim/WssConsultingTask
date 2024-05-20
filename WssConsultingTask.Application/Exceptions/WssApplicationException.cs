namespace WssConsultingTask.Application.Exceptions
{
    public class WssApplicationException : Exception
    {
        public ApplicationErrorCode ApplicationErrorCode { get; set; } = ApplicationErrorCode.Unknown;

        public WssApplicationException() : base()
        {

        }

        public WssApplicationException(string? message) : base(message)
        {

        }

        public WssApplicationException(
            ApplicationErrorCode errorCode,
            string? message,
            Exception? innerException = null) : base(message, innerException)
        {
            ApplicationErrorCode = errorCode;
        }
    }
}
