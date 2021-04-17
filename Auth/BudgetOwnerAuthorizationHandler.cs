using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace BudgetingApp.Auth
{
    public class BudgetOwnerAuthorizationHandler: AuthorizationHandler<BudgetOwnerRequirement, BudgetOwnerAuthorization>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BudgetOwnerRequirement requirement, BudgetOwnerAuthorization resource)
        {
            if (resource.BudgetUserId == resource.CurrentUserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }
}