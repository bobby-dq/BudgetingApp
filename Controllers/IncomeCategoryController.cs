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
    public class IncomeCategoryController: Controller
    {
        private BudgetingContext context;
        public IncomeCategoryController (BudgetingContext ctx)
        {
            context = ctx;
        }

        // HTTP Get Request
        // A breadown of a user's Income Category
        public async Task<IActionResult> Breakdown (long id)
        {
            IncomeCategory incomeCategory = await context.IncomeCategories
                .FirstAsync(ic => ic.IncomeCategoryId == id);

            Budget budget = await context.Budgets
                .FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            
            IQueryable<IncomeItem> incomeItems = context.IncomeItems
                .Where(ii => ii.IncomeCategoryId == id);

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
            Budget budget = await context.Budgets.FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            return View("IncomeCategoryEditor", IncomeCategoryFactory.Details(budget, incomeCategory));
        }

        // HTTP Get Request
        // Create a new Income Category
        public IActionResult Create (long id)
        {
            Budget budget = context.Budgets.Find(id);
            IncomeCategory incomeCategory = new IncomeCategory
            {
                BudgetId = id
            };
            
            return View("IncomeCategoryEditor", IncomeCategoryFactory.Create(budget, incomeCategory));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Create ([FromForm] IncomeCategory incomeCategory, long id)
        {
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
            Budget budget = await context.Budgets.FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            return View("IncomeCategoryEditor", IncomeCategoryFactory.Edit(budget, incomeCategory));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Edit ([FromForm]IncomeCategory incomeCategory, long id)
        {
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
            Budget budget = await context.Budgets.FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            return View("IncomeCategoryEditor", IncomeCategoryFactory.Delete(budget, incomeCategory));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Delete (IncomeCategory incomeCategory)
        {
            Budget preDeleteBudget = await context.Budgets.AsNoTracking().FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            context.IncomeCategories.Remove(incomeCategory);
            await context.SaveChangesAsync();
            return RedirectToAction("BudgetBreakdown", "Budget", new {id = preDeleteBudget.BudgetId});
        }
    }
}