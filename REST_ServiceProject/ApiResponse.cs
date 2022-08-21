using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace REST_ServiceProject
{
    public class ApiResponse
    {
        [JsonPropertyName("status_code")]
        [JsonProperty("status_code")]

        public int StatusCode { get; }
        public string Message { get; }

        public ApiResponse(int StatusCode, string message = null)
        {
            StatusCode = StatusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return "Resource not found";
                case 500:
                    return "An unhandled error occurred";
                default:
                    return null;
            }
        }
    }
}