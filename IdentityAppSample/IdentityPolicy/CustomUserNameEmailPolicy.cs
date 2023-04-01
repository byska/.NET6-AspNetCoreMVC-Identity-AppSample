using IdentityAppSample.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityAppSample.IdentityPolicy
{
    public class CustomUserNameEmailPolicy:UserValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            IdentityResult result= await base.ValidateAsync(manager, user);
            List<IdentityError> errors = new List<IdentityError>();
            if (user.UserName == "google")
            {
                errors.Add(new IdentityError
                {
                    Description = "Google cannot be used as username."
                });
            }
            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
