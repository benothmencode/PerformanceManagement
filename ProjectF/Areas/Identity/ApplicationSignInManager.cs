using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Areas.Identity
{
    public class ApplicationSignInManager: SignInManager<User>
    {

        private readonly UserManager<User> _userManager;
        public ApplicationSignInManager(UserManager<User>  userManager, IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<User> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<User>> logger,
        IAuthenticationSchemeProvider schemeProvider,
        IUserConfirmation<User> userConfirmation )
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemeProvider, userConfirmation)
        {
            _userManager = userManager;
        }
        public override Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(User user1, string password, bool rememberMe, bool shouldLockout)
        {
            var user =  _userManager.FindByIdAsync(user1.Id.ToString()).Result;

            if (!user.Active)
            {
                 return  Task.FromResult(SignInResult.Failed);
            }

            return base.PasswordSignInAsync(user1, password, rememberMe, shouldLockout);
        }
    }
}
