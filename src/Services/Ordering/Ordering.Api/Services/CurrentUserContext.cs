using Ordering.Application.Contracts;
using System.Security.Claims;

namespace Ordering.Api.Services;

public class CurrentUserContext : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCurrentUserName()
        => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
}