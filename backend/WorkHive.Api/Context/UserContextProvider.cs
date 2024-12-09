using System.Security.Claims;
using WorkHive.Application.Abstraction.Context;

namespace WorkHive.Api.Context
{
    public class UserContextProvider(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "SystemUser";
    }
}