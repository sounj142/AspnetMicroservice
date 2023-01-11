using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IList<IValidator<TRequest>> _validators;

    public ValidationBehavior(IList<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var validationContext = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validators.Select(x => x.ValidateAsync(validationContext, cancellationToken)));

            var errors = validationResults
                .Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .ToList();
            if (errors.Count > 0)
                throw new Exceptions.ValidationException(errors);
        }
        return await next();
    }
}