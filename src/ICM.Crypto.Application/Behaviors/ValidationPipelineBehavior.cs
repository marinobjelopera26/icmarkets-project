using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ICM.Crypto.Application.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public ValidationPipelineBehavior(
        IServiceProvider serviceProvider, 
        ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validator = _serviceProvider.GetService<IValidator<TRequest>>();
        if (validator is null)
        {
            _logger.LogInformation(
                "Skipping request validation: No validator found for '{Type}'.", typeof(TRequest).FullName);
            
            return await next(cancellationToken);
        }

        _logger.LogInformation("Running request validation for '{Type}'.", typeof(TRequest).FullName);
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Request validation failed for '{Type}': Error count: {ErrorCount}.", 
                typeof(TRequest).FullName, 
                validationResult.Errors.Count);

            throw new ValidationException(validationResult.Errors);
        }
        
        _logger.LogInformation("Request validation succeeded for '{Type}'.", typeof(TRequest).FullName);
        return await next(cancellationToken);
    }
}