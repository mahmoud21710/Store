using Microsoft.AspNetCore.Http;
using Store.G04.APIs.Error;
using System.Text.Json;

namespace Store.G04.APIs.MiddeelWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next ,ILogger<ExceptionMiddleWare> logger , IHostEnvironment env )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try 
            {
                await _next.Invoke(context);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = _env.IsDevelopment() ?
                    new APiExceptionRsponse(StatusCodes.Status500InternalServerError, ex?.StackTrace.ToString(), ex?.Message)
                    : new APiExceptionRsponse(StatusCodes.Status500InternalServerError);

                var res=  JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(res);
            }
        }

    }
}
