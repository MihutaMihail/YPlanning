using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YPlanning.Interfaces.Services;
using YPlanning.Models;

namespace YPlanning.Authorize
{
    public class AuthorizeRoleAttribute : TypeFilterAttribute
    {
        public AuthorizeRoleAttribute(params string[] roles) : base(typeof(AuthorizeRoleFilter))
        {
            Arguments = new object[] { roles };
        }
    }

    public class AuthorizeRoleFilter : IAuthorizationFilter
    {
        private readonly string[] _roles;

        public AuthorizeRoleFilter(string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenService = (ITokenService)context.HttpContext.RequestServices.GetService(typeof(ITokenService))!;
            if (tokenService == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenValue = context.HttpContext.Request.Headers["yplanning-key"].FirstOrDefault();
            if (string.IsNullOrEmpty(tokenValue))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if token is invalid
            if (!tokenService.DoesTokenExist(tokenValue))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Get existing token
            var existingToken = tokenService.GetTokenByValue(tokenValue);
            if (existingToken == null || existingToken.Equals(new Token()))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if the token associated with the user
            // Has the necessary privileges to access based on role
            if (_roles != null && _roles.Length > 0)
            {
                bool roleMatched = false;
                foreach (var role in _roles)
                {
                    if (existingToken.Role == role)
                    {
                        roleMatched = true;
                        break;
                    }
                }
                
                if (!roleMatched)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}
