namespace Store.G04.APIs.Error
{
    public class ApiValidationErrorResponse : ApiErrorResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }

        public IEnumerable<string> Erros { get; set; } = new List<string>();
    }
}
