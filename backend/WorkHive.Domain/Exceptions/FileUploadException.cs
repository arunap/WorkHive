namespace WorkHive.Domain.Exceptions
{
    public class FileUploadException : Exception
    {
        public FileUploadException() { }
        public FileUploadException(string message) : base(message) { }
        public FileUploadException(string message, Exception innerException) : base(message, innerException) { }
    }
}