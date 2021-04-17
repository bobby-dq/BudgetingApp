using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BudgetingApp.Models.RepositoryModels;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;
using BudgetingApp.Models.ViewModelFactories;

namespace BudgetingApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class IncomeCategoryController: Controller
    {
        private BudgetingContext context;
        private UserManager<IdentityUser> userManager;
        private string GetUserId() => userManager.GetUserId(User);
        private bool IsOwner(string userId) => userId == GetUserId();
        public IncomeCategoryController (BudgetingContext ctx, UserManager<IdentityUser> userMgr)
        {
            userManager = userMgr;
            context = ctx;
        }

        // HTTP Get Request
        // A breadown of a user's Income Category
        public async Task<IActionResult> Breakdown (long id)
        {
            IncomeCategory incomeCategory = await context.IncomeCategories
                .FirstAsync(ic => ic.IncomeCategoryId == id);

            if(!IsOwner(incomeCategory.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            Budget budget = await context.Budgets
                .FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            
            IQueryable<IncomeItem> incomeItems = context.IncomeItems
                .Where(ii => ii.IncomeCategoryId == id)
                .OrderByDescending(ii => ii.TransactionDate);

            IncomeCategoryBreakdownViewModel incomeCategoryBreakdownViewModel = new IncomeCategoryBreakdownViewModel 
            {
                Budget = budget,
                IncomeCategory = incomeCategory,
                IncomeItems = incomeItems
            };

            return View("Breakdown", incomeCategoryBreakdownViewModel);
        }

        // HTTP Get Request
        // Shows details of an income category
        public async Task<IActionResult> Details (long id)
        {
            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(id);

            if (!IsOwner(incomeCategory.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            Budget budget = await context.Budgets.FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            return View("IncomeCategoryEditor", IncomeCategoryFactory.Details(budget, incomeCategory));
        }

        // HTTP Get Request
        // Create a new Income Category
        public IActionResult Create (long id)
        {
            Budget budget = context.Budgets.Find(id);

            if (!IsOwner(budget.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            IncomeCategory incomeCategory = new IncomeCategory
            {
                UserId = GetUserId(),
                BudgetId = id
            };
            
            return View("IncomeCategoryEditor", IncomeCategoryFactory.Create(budget, incomeCategory));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Create ([FromForm] IncomeCategory incomeCategory, long id)
        {
            if (!IsOwner(incomeCategory.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            Budget preSaveBudget = await context.Budgets.AsNoTracking().FirstAsync(b => b.BudgetId == id);
            
            if (ModelState.IsValid)
            {
                incomeCategory.IncomeCategoryId = default;
                incomeCategory.Budget = default;
                context.IncomeCategories.Add(incomeCategory);
                await context.SaveChangesAsync();
                return RedirectToAction("BudgetBreakdown", "Budget", new {id = preSaveBudget.BudgetId});
            }

            return View("IncomeCategoryEditor", IncomeCategoryFactory.Create(preSaveBudget, incomeCategory));
        }

        // HTTP Get Request
        // Edits an income category
        public async Task<IActionResult> Edit (long id)
        {
            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(id);

            if (!IsOwner(incomeCategory.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            Budget budget = await context.Budgets.FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            return View("IncomeCategoryEditor", IncomeCategoryFactory.Edit(budget, incomeCategory));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Edit ([FromForm]IncomeCategory incomeCategory, long id)
        {
            if (!IsOwner(incomeCategory.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            Budget preSaveBudget = await context.Budgets.AsNoTracking().FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            if (ModelState.IsValid)
            {
                context.IncomeCategories.Update(incomeCategory);
                await context.SaveChangesAsync();
                return RedirectToAction("BudgetBreakdown", "Budget", new {id = preSaveBudget.BudgetId});
            }
            return View("IncomeCategoryEditor", IncomeCategoryFactory.Edit(preSaveBudget, incomeCategory));
        }

        // HTTP Get Request
        // Delete an income category
        public async Task<IActionResult> Delete (long id)
        {
            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(id);

            if (!IsOwner(incomeCategory.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            Budget budget = await context.Budgets.FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            return View("IncomeCategoryEditor", IncomeCategoryFactory.Delete(budget, incomeCategory));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Delete (IncomeCategory incomeCategory)
        {
            if (!IsOwner(incomeCategory.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }
            
            Budget preDeleteBudget = await context.Budgets.AsNoTracking().FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            context.IncomeCategories.Remove(incomeCategory);
            await context.SaveChangesAsync();
            return RedirectToAction("BudgetBreakdown", "Budget", new {id = preDeleteBudget.BudgetId});
        }
    }
}