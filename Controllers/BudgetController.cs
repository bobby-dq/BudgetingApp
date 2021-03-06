using System;
using System.Threading.Tasks;
using System.Linq;
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
    public class BudgetController: Controller
    {
        private BudgetingContext context;
        private UserManager<IdentityUser> userManager;
        private DateTime GetFirstDayOfMonth() => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private DateTime GetLastDayOfMonth() => GetFirstDayOfMonth().AddMonths(1).AddDays(-1);
        private string GetCurrentMonthAndYear() => GetFirstDayOfMonth().ToString("MMMM yyyy");
        private string GetUserId() => userManager.GetUserId(User);
        private bool IsOwner(string userId) => userId == GetUserId();
        public BudgetController (BudgetingContext ctx, UserManager<IdentityUser> userMgr)
        {
            userManager = userMgr;
            context = ctx;
        }

        // HTTP Get Request
        // Lists out a users budget list.
        public IActionResult Index()
        {
            IQueryable<Budget> budgets = context.Budgets
                .Include(b => b.ExpenseItems)
                .Include(b => b.IncomeItems)
                .AsSplitQuery()
                .Where(b => b.UserId == GetUserId())
                .OrderByDescending(b => b.StartDate);
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

            if (!IsOwner(budget.UserId))
            {
                return RedirectToPage("/Error/Error404");
            };

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
        // Shows a list of the transactions (expense and income items)
        public async Task<IActionResult> Transactions (long id)
        {
            Budget budget = await context.Budgets.FirstAsync(b => b.BudgetId == id);

            if (!IsOwner(budget.UserId))
            {
                return RedirectToPage("/Error/Error404");
            };
            
            IQueryable<ExpenseItem> expenseItems = context.ExpenseItems
                .Where(ei => ei.BudgetId == id)
                .OrderByDescending(ei => ei.TransactionDate);

            IQueryable<IncomeItem> incomeItems = context.IncomeItems
                .Where(ii => ii.BudgetId == id)
                .OrderByDescending(ii => ii.TransactionDate);


            return View("BudgetTransaction", BudgetFactory.Transactions(budget, incomeItems, expenseItems));
            
        }

        // HTTP Get Request
        // Shows a detail of a budget
        public async Task<IActionResult> Details (long id)
        {
            Budget budget = await context.Budgets.FindAsync(id);

            if (!IsOwner(budget.UserId))
            {
                return RedirectToPage("/Error/Error404");
            };


            return View("BudgetEditor", BudgetFactory.Details(budget));
        }

        // HTTP Get Request
        // Creates a new Budget
        public IActionResult Create() 
        {
            Budget budget = new Budget 
            {
                UserId = GetUserId(),
                Description = $"{GetCurrentMonthAndYear()} Budget",
                StartDate = GetFirstDayOfMonth(),
                EndDate = GetLastDayOfMonth()
            };
            return View("BudgetEditor", BudgetFactory.Create(budget));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Budget budget)
        {
            if (!IsOwner(budget.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            if (ModelState.IsValid)
            {
                budget.BudgetId = default;
                context.Budgets.Add(budget);
                await context.SaveChangesAsync();
                return RedirectToAction("BudgetBreakdown", "Budget", new {id = budget.BudgetId});
            }
            return View("BudgetEditor", BudgetFactory.Create(budget));
        }

        // HTTP Get Request
        // Edits a budget
        public async Task<IActionResult> Edit(long id)
        {
            Budget budget = await context.Budgets.FindAsync(id);

            if (!IsOwner(budget.UserId))
            {
                return RedirectToPage("/Error/Error404");
            };

            return View("BudgetEditor", BudgetFactory.Edit(budget));
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> Edit(long id, [FromForm] Budget budget)
        {
            if (!IsOwner(budget.UserId))
            {
                return RedirectToPage("/Error/Error404");
            }

            Budget preSaveBudget = await context.Budgets.AsNoTracking().FirstAsync(b => b.BudgetId == id);

            if (ModelState.IsValid)
            {
                context.Budgets.Update(budget);
                await context.SaveChangesAsync();
                return RedirectToAction("BudgetBreakdown", "Budget", new {id = budget.BudgetId});
            }
            return View("BudgetEditor", BudgetFactory.Edit(preSaveBudget));
        }

        // HTTP Get Request
        // Delete a budget
        public async Task<IActionResult> Delete (long id)
        {
            Budget budget = await context.Budgets.FindAsync(id);

            if (!IsOwner(budget.UserId))
            {
                return RedirectToPage("/Error/Error404");
            };


            return View("BudgetEditor", BudgetFactory.Delete(budget));
        }

        // HTTP Post Request
        [HttpPost]
        public async Task<IActionResult> Delete(Budget budget)
        {
            if (!IsOwner(budget.UserId))
            {
                return RedirectToPage("/Error/Error404");
            };

            context.Budgets.Remove(budget);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

