using IdentityAppSample.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityAppSample.IdentityPolicy
{
    public class CustomPasswordPolicy : PasswordValidator<AppUser>
    {
        public async override Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            IdentityResult result = await base.ValidateAsync(manager, user, password);
            List<IdentityError> errors = new List<IdentityError>();
            if(password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Description = "Password cannot contain username."
                });
            }
            if(password.Contains("123"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Password cannot contain 123 numeric sequance."
                });
            }
            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
