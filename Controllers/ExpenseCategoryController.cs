using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BudgetingApp.Models.RepositoryModels;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;
using BudgetingApp.Models.ViewModelFactories;

namespace BudgetingApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ExpenseCategoryController: Controller
    {
        private BudgetingContext context;
        private UserManager<IdentityUser> userManager;
        private string GetUserId() => userManager.GetUserId(User);
        public ExpenseCategoryController (BudgetingContext ctx, UserManager<IdentityUser> userMgr)
        {
            context = ctx;
            userManager = userMgr;
        }

        // HTTP Get Request
        // A breadown of a user's Expense Category
        public async Task<IActionResult> Breakdown (long id)
        {
            ExpenseCategory expenseCategory = await context.ExpenseCategories
                .FirstAsync(ec => ec.ExpenseCategoryId == id);

            Budget budget = await context.Budgets
                .FirstAsync(b => b.BudgetId == expenseCategory.BudgetId);
            
            IQueryable<ExpenseItem> incomeItems = context.ExpenseItems
                .Where(ei => ei.ExpenseCategoryId == id)
                .OrderByDescending(ei=> ei.TransactionDate);

            ExpenseCategoryBreakdownViewModel expenseCategoryBreakdownViewModel = new ExpenseCategoryBreakdownViewModel 
            {
                Budget = budget,
                ExpenseCategory = expenseCategory,
                ExpenseItems = incomeItems
            };

            return View("Breakdown", expenseCategoryBreakdownViewModel);
        }

        // HTTP Get Request
        // Shows details of an income category
        public async Task<IActionResult> Details (long id)
        {
            ExpenseCategory expenseCategory = await context.ExpenseCategories.FindAsync(id);
            Budget budget = await context.Budgets.FirstAsync(b => b.BudgetId == expenseCategory.BudgetId);
            return View("ExpenseCategoryEditor", ExpenseCategoryFactory.Details(budget, expenseCategory));
        }

        // HTTP Get Request
        // Create a new Income Category
        public IActionResult Create (long id)
        {
            Budget budget = context.Budgets.Find(id);
            ExpenseCategory expenseCategory = new ExpenseCategory
            {
                UserId = GetUserId(),
                BudgetId = id
            };
            
            return View("ExpenseCategoryEditor", ExpenseCategoryFactory.Create(budget, expenseCategory));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Create ([FromForm] ExpenseCategory expenseCategory, long id)
        {
            Budget preSaveBudget = await context.Budgets.AsNoTracking().FirstAsync(b => b.BudgetId == id);
            
            if (ModelState.IsValid)
            {
                expenseCategory.ExpenseCategoryId = default;
                expenseCategory.Budget = default;
                context.ExpenseCategories.Add(expenseCategory);
                await context.SaveChangesAsync();
                return RedirectToAction("BudgetBreakdown", "Budget", new {id = preSaveBudget.BudgetId});
            }

            return View("ExpenseCategoryEditor", ExpenseCategoryFactory.Create(preSaveBudget, expenseCategory));
        }

        // HTTP Get Request
        // Edits an income category
        public async Task<IActionResult> Edit (long id)
        {
            ExpenseCategory expenseCategory = await context.ExpenseCategories.FindAsync(id);
            Budget budget = await context.Budgets.FirstAsync(b => b.BudgetId == expenseCategory.BudgetId);
            return View("ExpenseCategoryEditor", ExpenseCategoryFactory.Edit(budget, expenseCategory));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Edit ([FromForm]ExpenseCategory expenseCategory)
        {
            Budget preSaveBudget = await context.Budgets.AsNoTracking().FirstAsync(b => b.BudgetId == expenseCategory.BudgetId);
            if (ModelState.IsValid)
            {
                context.ExpenseCategories.Update(expenseCategory);
                await context.SaveChangesAsync();
                return RedirectToAction("BudgetBreakdown", "Budget", new {id = preSaveBudget.BudgetId});
            }
            return View("ExpenseCategoryEditor", ExpenseCategoryFactory.Edit(preSaveBudget, expenseCategory));
        }

        // HTTP Get Request
        // Delete an income category
        public async Task<IActionResult> Delete (long id)
        {
            ExpenseCategory expenseCategory = await context.ExpenseCategories.FindAsync(id);
            Budget budget = await context.Budgets.FirstAsync(b => b.BudgetId == expenseCategory.BudgetId);
            return View("ExpenseCategoryEditor", ExpenseCategoryFactory.Delete(budget, expenseCategory));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Delete (ExpenseCategory expenseCategory)
        {
            Budget preDeleteBudget = await context.Budgets.AsNoTracking().FirstAsync(b => b.BudgetId == expenseCategory.BudgetId);
            context.ExpenseCategories.Remove(expenseCategory);
            await context.SaveChangesAsync();
            return RedirectToAction("BudgetBreakdown", "Budget", new {id = preDeleteBudget.BudgetId});
        }
    }
}