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
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class IncomeItemController:Controller
    {
        private BudgetingContext context;
        private UserManager<IdentityUser> userManager;
        private string GetUserId() => userManager.GetUserId(User);
        private bool IsOwner(string userId) => userId == GetUserId(); 
        public IncomeItemController(BudgetingContext ctx, UserManager<IdentityUser> userMgr)
        {
            userManager = userMgr;
            context = ctx;
        }

        // HTTP Get Request
        // Shows details of an income item
        public async Task<IActionResult> Details (long id)
        {
            IncomeItem incomeItem = await context.IncomeItems.FindAsync(id);

            if (!IsOwner(incomeItem.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(incomeItem.IncomeCategoryId);
            Budget budget = await context.Budgets.FindAsync(incomeItem.BudgetId);
            return View("IncomeItemEditor", IncomeItemFactory.Details(budget, incomeCategory, incomeItem));
        }

        // HTTP Get Request
        // Create a new Income Item
        public async Task<IActionResult> Create (long id)
        {
            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(id);

            if (!IsOwner(incomeCategory.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            Budget budget = await context.Budgets.FindAsync(incomeCategory.BudgetId);
            IncomeItem incomeItem = new IncomeItem
            {
                UserId = GetUserId(),
                IncomeCategoryId = id,
                BudgetId = budget.BudgetId,
                Description = $"{incomeCategory.Description} Transaction Item",
                TransactionDate = DateTime.Now
            };

            return View("IncomeItemEditor", IncomeItemFactory.Create(budget, incomeCategory, incomeItem));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] IncomeItem incomeItem, long id)
        {
            if (!IsOwner(incomeItem.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            IncomeCategory preSaveIncomeCategory = await context.IncomeCategories
                .AsNoTracking()
                .FirstAsync(ic => ic.IncomeCategoryId == id);
            Budget preSaveBudget = await context.Budgets
                .AsNoTracking()
                .FirstAsync(b => b.BudgetId == preSaveIncomeCategory.BudgetId);

            if (ModelState.IsValid)
            {
                incomeItem.IncomeItemId = default;
                incomeItem.Budget = default;
                incomeItem.IncomeCategory = default;
                context.IncomeItems.Add(incomeItem);
                await context.SaveChangesAsync();
                return RedirectToAction("BudgetBreakdown", "Budget", new {id = preSaveBudget.BudgetId});
            }

            return View("IncomeItemEditor", IncomeItemFactory.Create(preSaveBudget, preSaveIncomeCategory, incomeItem));
        }

        // HTTP Get Request
        // Edit an income item
        public async Task<IActionResult> Edit(long id)
        {   
            IncomeItem incomeItem = await context.IncomeItems.FindAsync(id);

            if (!IsOwner(incomeItem.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(incomeItem.BudgetId);
            Budget budget = await context.Budgets.FindAsync(incomeItem.BudgetId);
            
            return View("IncomeItemEditor", IncomeItemFactory.Edit(budget, incomeCategory, incomeItem));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] IncomeItem incomeItem)
        {
            if (!IsOwner(incomeItem.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            IncomeCategory preSaveIncomeCategory = await context.IncomeCategories
                .AsNoTracking()
                .FirstAsync(ic => ic.IncomeCategoryId == incomeItem.IncomeCategoryId);
            Budget preSaveBudget = await context.Budgets
                .AsNoTracking()
                .FirstAsync(b => b.BudgetId == incomeItem.BudgetId);

            if (ModelState.IsValid)
            {
                context.IncomeItems.Update(incomeItem);
                await context.SaveChangesAsync();
                return RedirectToAction("Breakdown", "IncomeCategory", new {id = preSaveIncomeCategory.IncomeCategoryId});
            }

            return View("IncomeItemEditor", IncomeItemFactory.Edit(preSaveBudget, preSaveIncomeCategory, incomeItem));
        }

        // HTTP Get request
        public async Task<IActionResult> Delete (long id)
        {
            IncomeItem incomeItem = await context.IncomeItems.FindAsync(id);

            if (!IsOwner(incomeItem.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(incomeItem.BudgetId);
            Budget budget = await context.Budgets.FindAsync(incomeItem.BudgetId);
            
            return View("IncomeItemEditor", IncomeItemFactory.Delete(budget, incomeCategory, incomeItem));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Delete (IncomeItem incomeItem)
        {
            if (!IsOwner(incomeItem.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }
            
            Budget preDeleteBudget = await context.Budgets
                .AsNoTracking()
                .FirstAsync(b => b.BudgetId == incomeItem.BudgetId);
            IncomeCategory preDeleteIncomeCategory = await context.IncomeCategories
                .AsNoTracking()
                .FirstAsync(ic => ic.IncomeCategoryId == incomeItem.IncomeCategoryId);
            context.IncomeItems.Remove(incomeItem);
            await context.SaveChangesAsync();
            return RedirectToAction("Breakdown", "IncomeCategory", new {id = preDeleteIncomeCategory.IncomeCategoryId});
        }
    }
}