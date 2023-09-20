using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Notes.Application.Common.Exceptions;

namespace Notes.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Inovoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {

                await HandleExceptionsAsync(context, exception);
            }
        }

        private Task HandleExceptionsAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exception)
            {
                case FluentValidation.ValidationException validationException: //check this method in future?!number 9
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
}

