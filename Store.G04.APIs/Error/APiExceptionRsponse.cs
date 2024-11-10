namespace Store.G04.APIs.Error
{
    public class APiExceptionRsponse  :ApiErrorResponse
    {
        public string?  Detalis { get; set; }

        public APiExceptionRsponse(int statuscode , string? message = null , string? detalis =null) 
             : base(statuscode,message)
        {
            Detalis = detalis;
        }
    }
}
