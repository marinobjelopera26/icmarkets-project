using System.Text.Json;
using FluentValidation;
using ICM.Crypto.Domain.Primitives;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ICM.Crypto.WebApi.Middleware;

internal sealed class DefaultExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = StatusCodes.Status500InternalServerError;

        ProblemDetails problemDetails;
        if (exception is ValidationException validationException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            var validationErrors = validationException.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    grouping => grouping.Key, 
                    grouping => grouping.Select(y => y.ErrorMessage).ToArray());
            
            problemDetails = new ValidationProblemDetails(validationErrors);
        }
        else if (exception is DomainValidationException domainValidationException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            problemDetails = new ProblemDetails
            {
                Title = "Domain validation rule violation",
                Detail = domainValidationException.Message,
                Status = statusCode,
            };
        }
        else
        {
            problemDetails = new ProblemDetails
            {
                Title = "Unexpected error occurred",
                Detail = exception.Message,
                Status = statusCode,
            };
        }

        httpContext.Response.Clear();
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}