namespace Ordering.Application.Contracts;

public class ManualCurrentUserContext : ICurrentUserContext
{
    public string? CurrentUserName { get; set; } = null;

    public string? GetCurrentUserName() => CurrentUserName;
}