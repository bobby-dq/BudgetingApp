using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;

namespace BudgetingApp.Components
{
    public class AuthenticatedNavBar: ViewComponent
    {
        private UserManager<IdentityUser> userManager;
        public AuthenticatedNavBar (UserManager<IdentityUser> usrMgr)
        {
            userManager = usrMgr;
        }
        public IViewComponentResult Invoke()
        {
            AuthenticatedNavBarModel userModel = new AuthenticatedNavBarModel
            {
                Name = userManager.GetUserName((System.Security.Claims.ClaimsPrincipal) User),
                Id = userManager.GetUserId((System.Security.Claims.ClaimsPrincipal) User)
            };          
            return View(userModel);
        }
    }
}