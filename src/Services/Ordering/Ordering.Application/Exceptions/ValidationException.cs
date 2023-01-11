using FluentValidation.Results;

namespace Ordering.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures have occurred.")
    {
        Errors = failures.GroupBy(x => x.PropertyName)
            .ToDictionary(
                x => x.Key,
                x => x.Select(x => x.ErrorMessage).ToArray()
            );
    }
}