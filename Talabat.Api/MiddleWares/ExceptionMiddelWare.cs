using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Api.Errors;

namespace Talabat.Api.MiddleWares
{
    public class ExceptionMiddelWare
    {
        private readonly ILogger<ExceptionMiddelWare> logger;
        private readonly IHostEnvironment environment;
        private readonly RequestDelegate requestDelegate;
        public ExceptionMiddelWare(IHostEnvironment environment,ILogger<ExceptionMiddelWare> logger, RequestDelegate requestDelegate)
        {
            this.logger = logger;
            this.environment = environment;
            this.requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext Context)
        {
            try {
                await requestDelegate.Invoke(Context);
            }
            catch ( Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Context.Response.ContentType = "application/json";
                Context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                var ExceptionResponse = environment.IsDevelopment() ? new ApiExceptionResponse(500, ex.StackTrace, ex.Message)
                    : new ApiExceptionResponse(500);
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json  = JsonSerializer.Serialize(ExceptionResponse, options);
                await Context.Response.WriteAsync(json);
            }
        }
    }
}
