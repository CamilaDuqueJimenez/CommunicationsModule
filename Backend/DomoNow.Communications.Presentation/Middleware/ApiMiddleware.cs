using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Text.Json;

namespace DomoNow.Communications.Presentation.Middleware
{
    public class ApiMiddleware(ILogger<ApiMiddleware> pLogger, ProblemDetailsFactory problemDetailsFactory) : IMiddleware
    {
        private readonly ILogger<ApiMiddleware> _Logger = pLogger;
        private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception pException)
            {
                _Logger.LogError(pException, pException.Message ?? pException.InnerException?.Message);
                ProblemDetails problem = _problemDetailsFactory.CreateProblemDetails(
                    context,
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    title: "Server Error",
                    detail: "Ha ocurrido un error en el servidor");
                string resultJson = JsonSerializer.Serialize(problem);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(resultJson);
            }
        }
    }
}