namespace Talabat.Api.Errors
{
    public class ApiResponse
    {
        public int statusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            this.statusCode = statusCode;
            Message = message ?? returnErrorMessage(statusCode);
        }
        private string returnErrorMessage(int status)
        {
            return status switch
            {
                400 => "Bad request",
                401 => "you are not autherized",
                502 => "",
                _ => null
            };
        }
    }
}
