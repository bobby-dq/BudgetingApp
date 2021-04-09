using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetingApp.Models.RepositoryModels;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;
using BudgetingApp.Models.ViewModelFactories;

namespace BudgetingApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class IncomeItemController:Controller
    {
        private BudgetingContext context;
        public IncomeItemController(BudgetingContext ctx)
        {
            context = ctx;
        }

        // HTTP Get Request
        // Shows details of an income item
        public async Task<IActionResult> Details (long id)
        {
            IncomeItem incomeItem = await context.IncomeItems.FindAsync(id);
            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(incomeItem.IncomeCategoryId);
            Budget budget = await context.Budgets.FindAsync(incomeItem.BudgetId);
            return View("IncomeItemEditor", IncomeItemFactory.Details(budget, incomeCategory, incomeItem));
        }

        // HTTP Get Request
        // Create a new Income Item
        public async Task<IActionResult> Create (long id)
        {
            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(id);
            Budget budget = await context.Budgets.FindAsync(incomeCategory.BudgetId);
            IncomeItem incomeItem = new IncomeItem
            {
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
            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(incomeItem.BudgetId);
            Budget budget = await context.Budgets.FindAsync(incomeItem.BudgetId);
            
            return View("IncomeItemEditor", IncomeItemFactory.Edit(budget, incomeCategory, incomeItem));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] IncomeItem incomeItem)
        {
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
                return RedirectToAction("BudgetBreakdown", "Budget", new {id = preSaveBudget.BudgetId});
            }

            return View("IncomeItemEditor", IncomeItemFactory.Edit(preSaveBudget, preSaveIncomeCategory, incomeItem));
        }

        // HTTP Get request
        public async Task<IActionResult> Delete (long id)
        {
            IncomeItem incomeItem = await context.IncomeItems.FindAsync(id);
            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(incomeItem.BudgetId);
            Budget budget = await context.Budgets.FindAsync(incomeItem.BudgetId);
            
            return View("IncomeItemEditor", IncomeItemFactory.Delete(budget, incomeCategory, incomeItem));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Delete (IncomeItem incomeItem)
        {
            Budget preDeleteBudget = await context.Budgets
                .AsNoTracking()
                .FirstAsync(b => b.BudgetId == incomeItem.BudgetId);
            IncomeCategory preDeleteIncomeCategory = await context.IncomeCategories
                .AsNoTracking()
                .FirstAsync(ic => ic.IncomeCategoryId == incomeItem.IncomeCategoryId);
            context.IncomeItems.Remove(incomeItem);
            await context.SaveChangesAsync();
            return RedirectToAction("BudgetBreakdown", "Budget", new {id = preDeleteBudget.BudgetId});
        }
    }
}