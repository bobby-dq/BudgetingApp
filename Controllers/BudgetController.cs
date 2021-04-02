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
    public class BudgetController: Controller
    {
        private BudgetingContext context;
        private DateTime GetFirstDayOfMonth() => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private DateTime GetLastDayOfMonth() => GetFirstDayOfMonth().AddMonths(1).AddDays(-1);
        private String GetCurrentMonthAndYear() => GetFirstDayOfMonth().ToString("MMMM yyyy");
        public BudgetController (BudgetingContext ctx)
        {
            context = ctx;
        }

        // HTTP Get Request
        // Lists out a users budget list.
        public IActionResult Index()
        {
            IQueryable<Budget> budgets = context.Budgets
                .Include(b => b.ExpenseItems)
                .Include(b => b.IncomeItems)
                .AsSplitQuery();
            return View("Index", budgets);
        }

        // HTTP Get Request
        // A breakdown of a user's budget
        public async Task<IActionResult> BudgetBreakdown(long id)
        {
            Budget budget = await context.Budgets
                .Include(ec => ec.ExpenseItems)
                .Include(ic => ic.IncomeItems)
                .AsSplitQuery()
                .FirstAsync(b => b.BudgetId == id);

            IQueryable<ExpenseCategory> expenseCategories =  context.ExpenseCategories
                .Include(ec => ec.ExpenseItems)
                .AsSplitQuery()
                .Where(ec => ec.BudgetId == id);

            IQueryable<IncomeCategory> incomeCategories =  context.IncomeCategories
                .Include(ic => ic.IncomeItems)
                .AsSplitQuery()
                .Where(ic => ic.BudgetId == id);
            
            BudgetBreakdownViewModel budgetBreakdownViewModel = new BudgetBreakdownViewModel
            {
                Budget = budget,
                ExpenseCategories = expenseCategories,
                IncomeCategories = incomeCategories
            };
            
            return View("BudgetBreakdown", budgetBreakdownViewModel);
        }

        // HTTP Get Request
        // Shows a detail of a budget
        public async Task<IActionResult> Details (long id)
        {
            Budget budget = await context.Budgets.FindAsync(id);
            return View("BudgetEditor", BudgetFactory.Details(budget));
        }

        // HTTP Get Request
        // Creates a new Budget
        public IActionResult Create() 
        {
            Budget budget = new Budget 
            {
                Description = $"{GetCurrentMonthAndYear()} Budget",
                StartDate = GetFirstDayOfMonth(),
                EndDate = GetLastDayOfMonth(),
            };
            return View("BudgetEditor", BudgetFactory.Create(budget));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Budget budget)
        {
            if (ModelState.IsValid)
            {
                budget.BudgetId = default;
                context.Budgets.Add(budget);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("BudgetEditor", BudgetFactory.Create(budget));
        }

        // HTTP Get Request
        // Edits a budget
        public async Task<IActionResult> Edit(long id)
        {
            Budget budget = await context.Budgets.FindAsync(id);
            return View("BudgetEditor", BudgetFactory.Edit(budget));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Edit(long id, [FromForm] Budget budget)
        {
            Budget preSaveBudget = await context.Budgets.AsNoTracking().FirstAsync(b => b.BudgetId == id);
            if (ModelState.IsValid)
            {
                context.Budgets.Update(budget);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("BudgetEditor", BudgetFactory.Edit(preSaveBudget));
        }

        // HTTP Get Request
        // Delete a budget
        public async Task<IActionResult> Delete (long id)
        {
            Budget budget = await context.Budgets.FindAsync(id);
            return View("BudgetEditor", BudgetFactory.Delete(budget));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Delete(Budget budget)
        {
            context.Budgets.Remove(budget);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

