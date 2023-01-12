using Ordering.Application.Contracts;
using System.Security.Claims;

namespace Ordering.Api.Services;

public class CurrentUserContext : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ManualCurrentUserContext _manualCurrentUserContext;

    public CurrentUserContext(IHttpContextAccessor httpContextAccessor,
        ManualCurrentUserContext manualCurrentUserContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _manualCurrentUserContext = manualCurrentUserContext;
    }

    public string? GetCurrentUserName()
        => _manualCurrentUserContext.GetCurrentUserName()
        ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
}