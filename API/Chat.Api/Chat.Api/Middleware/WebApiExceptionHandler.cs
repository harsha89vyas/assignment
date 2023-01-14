using System;
using System.Net;

namespace Chat.Api.Middleware
{
	public class WebApiExceptionHandler
	{
        private readonly RequestDelegate _next;

        public WebApiExceptionHandler(RequestDelegate next)
		{
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                //Will be logging instead
                Console.WriteLine(ex);
                //We can multiple excepltion handling here and we can pass guid information for better tracking
                var response = httpContext.Response;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync("An error has occured. Please try again.");
            }
        }
	}
}

