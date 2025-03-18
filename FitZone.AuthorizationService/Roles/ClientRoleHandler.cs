using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.AuthorizationService.Roles
{
    public class ClientRoleHandler : AuthorizationHandler<ClientRoleRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientRoleRequirment requirement)
        {
            var rolesClaim = context.User.Claims
                .Where(claim => claim.Type.Contains("role"));

            if (rolesClaim != null && rolesClaim.Any(c => c.Value == "Client"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }

        public class ClientRoleRequirment : IAuthorizationRequirement { }

}
