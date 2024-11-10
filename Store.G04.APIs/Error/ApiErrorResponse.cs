namespace Store.G04.APIs.Error
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiErrorResponse(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            var message = statusCode switch
            {

                400 => "A Bad Request ,You Have made",
                401 => "Authorized , You r not",
                404 => "Resourses was NOt Found ",
                500 => "Server Error",
                _ => null
            };

            return message;
        }
    }
}
