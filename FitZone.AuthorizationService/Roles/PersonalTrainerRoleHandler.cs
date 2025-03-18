using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.AuthorizationService.Roles
{
    public class PersonalTrainerRoleHandler : AuthorizationHandler<PersonalTrainerRoleRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PersonalTrainerRoleRequirment requirement)
        {
            var rolesClaim = context.User.Claims
                .Where(claim => claim.Type.Contains("role"));

            if (rolesClaim != null && rolesClaim.Any(c => c.Value == "PersonalTrainer"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }

    public class PersonalTrainerRoleRequirment : IAuthorizationRequirement { }
}
