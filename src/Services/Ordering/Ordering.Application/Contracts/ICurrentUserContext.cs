namespace Ordering.Application.Contracts;

public interface ICurrentUserContext
{
    string? GetCurrentUserName();
}