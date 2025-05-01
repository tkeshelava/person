using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonManagment.Application.Exceptions;

namespace PersonManagement.Api.Configurations.Exceptions;

public class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = MapToProblemDetails(exception);
        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = MapToProblemDetails(exception)
        });
    }

    private ProblemDetails MapToProblemDetails(Exception exception)
    {
        return exception switch
        {
            ObjectNotFoundException notFoundException => new ProblemDetails
            {
                Type = "https://httpstatuses.com/404",
                Detail = notFoundException.Message,
                Title = "Resource not found.",
                Status = StatusCodes.Status404NotFound
            },
            ValidationException validationException => new ProblemDetails
            {
                Type = "https://httpstatuses.com/400",
                Detail = validationException.Message,
                Title = "Incorrect Request.",
                Status = StatusCodes.Status400BadRequest
            },
            _ => new ProblemDetails
            {
                Type = "https://httpstatuses.com/500",
                Detail = exception.Message,
                Title = "An error occurred while processing your request.",
                Status = StatusCodes.Status500InternalServerError
            }
        };
    }
}