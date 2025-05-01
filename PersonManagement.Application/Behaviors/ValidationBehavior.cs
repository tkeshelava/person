using FluentValidation;
using MediatR;

namespace PersonManagment.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .Select(f => f.ErrorMessage)
            .ToList();

        if (failures.Count != 0)
        {
            var errorMessage = failures.Aggregate((acc, n) => acc + "; " + n);
            throw new Exceptions.ValidationException(errorMessage);
        }

        return next();
    }
}