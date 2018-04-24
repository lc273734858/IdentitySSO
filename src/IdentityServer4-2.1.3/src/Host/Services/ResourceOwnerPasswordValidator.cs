using Host.Interfaces.Repositories;
using Host.Utilities;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Host.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public IUserService UserService { get; private set; }

        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            UserService = userService;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await UserService.GetAsync(context.UserName, context.Password);

            if (user != null)
            {
                var claims = ClaimsUtility.GetClaims(user);
                context.Result = new GrantValidationResult(user.Id.ToString(), "password", claims);
            }
        }
    }
}
