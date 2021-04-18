using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace BudgetingApp.Components
{
    public class AdministratorFooter: ViewComponent
    {
        private UserManager<IdentityUser> userManager;
        public AdministratorFooter (UserManager<IdentityUser> usrMgr)
        {
            userManager = usrMgr;
        }
        public IViewComponentResult Invoke()
        {
            AdministratorFooterModel footerModel = new AdministratorFooterModel
            {
                Name = userManager.GetUserName((System.Security.Claims.ClaimsPrincipal) User),
                Id = userManager.GetUserId((System.Security.Claims.ClaimsPrincipal) User)
            };

            return View(footerModel);
        }
    }
}