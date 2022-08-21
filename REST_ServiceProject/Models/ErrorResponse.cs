namespace REST_ServiceProject.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string Field { get; set; }
        public string DBCode { get; set; }
        public string Data { get; set; }
    }
}