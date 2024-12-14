using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using WorkHive.Application.Abstraction.Makers;
using WorkHive.Domain.Shared.StringConstants;

namespace WorkHive.Application.Abstraction.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse>(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null || httpContext.User.Identity == null || !httpContext.User.Identity.IsAuthenticated)
            {
                // Skip authorization for specific requests, e.g., LoginRequest
                if (request is IAllowAnonymous) return await next();

                throw new UnauthorizedAccessException("User is not authenticated");
            }

            // Handle role-based authorization based on request type
            if (request is IRequireAdminRole)
            {
                var result = await _authorizationService.AuthorizeAsync(httpContext.User, null, new RolesAuthorizationRequirement([ApplicationRoles.AdminRole]));
                if (!result.Succeeded) throw new UnauthorizedAccessException("User does not have Admin role.");
            }
            else if (request is IRequireEditorRole)
            {
                var result = await _authorizationService.AuthorizeAsync(httpContext.User, null, new RolesAuthorizationRequirement([ApplicationRoles.EditorRole]));
                if (!result.Succeeded) throw new UnauthorizedAccessException("User does not have Editor role.");
            }
            else if (request is IRequireViewerRole)
            {
                var result = await _authorizationService.AuthorizeAsync(httpContext.User, null, new RolesAuthorizationRequirement([ApplicationRoles.ViewerRole]));
                if (!result.Succeeded) throw new UnauthorizedAccessException("User does not have Viewer role.");
            }
            else if (request is IRequireApproverRole || request is IRequireManagerRole)
            {
                var result = await _authorizationService.AuthorizeAsync(httpContext.User, null, new RolesAuthorizationRequirement([ApplicationRoles.ApproverRole, ApplicationRoles.MangerRole]));
                if (!result.Succeeded) throw new UnauthorizedAccessException("User does not have Approver role.");
            }
            // else throw new UnauthorizedAccessException("User Action does not have valid permissions.");

            return await next();
        }
    }
}