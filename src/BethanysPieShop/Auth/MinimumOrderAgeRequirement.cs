using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BethanysPieShop.Auth
{
    public class MinimumOrderAgeRequirement : AuthorizationHandler<MinimumOrderAgeRequirement>, IAuthorizationRequirement
    {
        private readonly int _minimumOrderAge;

        public MinimumOrderAgeRequirement(int minimumOrderAge)
        {
            _minimumOrderAge = minimumOrderAge;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumOrderAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            var birthDate = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);

            var ageInYears = DateTime.Today.Year - birthDate.Year;

            if (ageInYears >= _minimumOrderAge)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
