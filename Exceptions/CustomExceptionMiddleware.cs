using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace My_Books.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next(httpcontext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpcontext, ex);
                
            }
        }

        private Task HandleExceptionAsync(HttpContext httpcontext, Exception ex)
        {
            httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpcontext.Response.ContentType="application/json";

            var response = new ErrorVM()
            {
                StatusCode = httpcontext.Response.StatusCode,
                Message = ex.Message,
                Path = httpcontext.Request.Path
            };
            return httpcontext.Response.WriteAsync(response.ToString());
        }
    }
}
