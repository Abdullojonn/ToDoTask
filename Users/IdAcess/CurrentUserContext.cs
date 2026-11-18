using System.Security.Claims;

namespace ToDoApp.Users.IdAcess;

public class CurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
{

    private string? _userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    private bool _isAuthenticated = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public int GetRequiredUserId()
    {
        if (!_isAuthenticated || string.IsNullOrEmpty(_userId))
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        return int.Parse(_userId);
    }
}
