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
    public class ExpenseItemController:Controller
    {
        private BudgetingContext context;
        public ExpenseItemController(BudgetingContext ctx)
        {
            context = ctx;
        }

        // HTTP Get Request
        // Shows details of an expense item
        public async Task<IActionResult> Details (long id)
        {
            ExpenseItem expenseItem = await context.ExpenseItems.FindAsync(id);
            ExpenseCategory expenseCategory = await context.ExpenseCategories.FindAsync(expenseItem.ExpenseCategoryId);
            Budget budget = await context.Budgets.FindAsync(expenseItem.BudgetId);
            return View("ExpenseItemEditor", ExpenseItemFactory.Details(budget, expenseCategory, expenseItem));
        }

        // HTTP Get Request
        // Create a new Expense Item
        public async Task<IActionResult> Create (long id)
        {
            ExpenseCategory expenseCategory = await context.ExpenseCategories.FindAsync(id);
            Budget budget = await context.Budgets.FindAsync(expenseCategory.BudgetId);
            ExpenseItem expenseItem = new ExpenseItem
            {
                ExpenseCategoryId = id,
                BudgetId = budget.BudgetId,
                Description = $"{expenseCategory.Description} Transaction Item",
                TransactionDate = DateTime.Now
            };

            return View("ExpenseItemEditor", ExpenseItemFactory.Create(budget, expenseCategory, expenseItem));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ExpenseItem expenseItem, long id)
        {
            ExpenseCategory preSaveExpenseCategory = await context.ExpenseCategories
                .AsNoTracking()
                .FirstAsync(ec => ec.ExpenseCategoryId == id);
            Budget preSaveBudget = await context.Budgets
                .AsNoTracking()
                .FirstAsync(b => b.BudgetId == preSaveExpenseCategory.BudgetId);

            if (ModelState.IsValid)
            {
                expenseItem.ExpenseItemId = default;
                expenseItem.Budget = default;
                expenseItem.ExpenseCategory = default;
                context.ExpenseItems.Add(expenseItem);
                await context.SaveChangesAsync();
                return RedirectToAction("BudgetBreakdown", "Budget", new {id = preSaveBudget.BudgetId});
            }

            return View("ExpenseItemEditor", ExpenseItemFactory.Create(preSaveBudget, preSaveExpenseCategory, expenseItem));
        }

        // HTTP Get Request
        // Edit an expense item
        public async Task<IActionResult> Edit(long id)
        {   
            ExpenseItem expenseItem = await context.ExpenseItems.FindAsync(id);
            ExpenseCategory expenseCategory = await context.ExpenseCategories.FindAsync(expenseItem.BudgetId);
            Budget budget = await context.Budgets.FindAsync(expenseItem.BudgetId);
            
            return View("ExpenseItemEditor", ExpenseItemFactory.Edit(budget, expenseCategory, expenseItem));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] ExpenseItem expenseItem)
        {
            ExpenseCategory preSaveExpenseCategory = await context.ExpenseCategories
                .AsNoTracking()
                .FirstAsync(ec => ec.ExpenseCategoryId == expenseItem.ExpenseCategoryId);
            Budget preSaveBudget = await context.Budgets
                .AsNoTracking()
                .FirstAsync(b => b.BudgetId == expenseItem.BudgetId);

            if (ModelState.IsValid)
            {
                context.ExpenseItems.Update(expenseItem);
                await context.SaveChangesAsync();
                return RedirectToAction("Breakdown", "ExpenseCategory", new {id = preSaveExpenseCategory.ExpenseCategoryId});
            }

            return View("ExpenseItemEditor", ExpenseItemFactory.Edit(preSaveBudget, preSaveExpenseCategory, expenseItem));
        }

        // HTTP Get request
        public async Task<IActionResult> Delete (long id)
        {
            ExpenseItem expenseItem = await context.ExpenseItems.FindAsync(id);
            ExpenseCategory expenseCategory = await context.ExpenseCategories.FindAsync(expenseItem.BudgetId);
            Budget budget = await context.Budgets.FindAsync(expenseItem.BudgetId);
            
            return View("ExpenseItemEditor", ExpenseItemFactory.Delete(budget, expenseCategory, expenseItem));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Delete (ExpenseItem expenseItem)
        {
            Budget preDeleteBudget = await context.Budgets
                .AsNoTracking()
                .FirstAsync(b => b.BudgetId == expenseItem.BudgetId);
            ExpenseCategory preDeleteExpenseCategory = await context.ExpenseCategories
                .AsNoTracking()
                .FirstAsync(ec => ec.ExpenseCategoryId == expenseItem.ExpenseCategoryId);
            context.ExpenseItems.Remove(expenseItem);
            await context.SaveChangesAsync();
            return RedirectToAction("Breakdown", "ExpenseCategory", new {id = preDeleteExpenseCategory.ExpenseCategoryId});
        }
    }
}