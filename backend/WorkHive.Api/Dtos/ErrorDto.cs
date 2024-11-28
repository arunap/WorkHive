namespace WorkHive.Api.Dtos
{
    public class ErrorDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string[] Errors { get; set; } = [];
    }
}