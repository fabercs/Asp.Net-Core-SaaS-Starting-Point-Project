using EMSApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Middlewares
{
    public class ExceptionHandlerMiddleware 
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ILoggerFactory loggerFactory) 
        {
            var logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
            try
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                logger.LogError(message: exception.Message, exception);
                var response = context.Response;
                switch (exception)
                {
                    case AppException appEx:
                    case ValidationException valEx:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = exception?.Message });
                await response.WriteAsync(result);
            }
            
        }
    }
}
