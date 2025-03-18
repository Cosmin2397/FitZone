using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.AuthorizationService.Roles
{
    public class AppManagerRoleHandler : AuthorizationHandler<AppManagerRoleRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AppManagerRoleRequirment requirement)
        {
            var rolesClaim = context.User.Claims
                .Where(claim => claim.Type.Contains("role"));

            if (rolesClaim != null && rolesClaim.Any(c => c.Value == "AppManager"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }

    public class AppManagerRoleRequirment : IAuthorizationRequirement { }
}
