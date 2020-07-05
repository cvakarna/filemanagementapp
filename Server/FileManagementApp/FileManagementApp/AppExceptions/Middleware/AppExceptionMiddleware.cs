using FileManagementApp.AppExceptions.Models;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FileManagementApp.AppExceptions.middleware
{
    public class AppExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public AppExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var exceptionType = exception.GetType();
            if (exceptionType == typeof(ResourceNotExistsException))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
           
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }


    }
}
