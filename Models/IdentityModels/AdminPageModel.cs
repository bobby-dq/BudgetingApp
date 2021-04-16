using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace BudgetingApp.Models.IdentityModels
{
    [Authorize(Roles="Admin")]
    public class AdminPageModel: PageModel
    {
        
    }
}