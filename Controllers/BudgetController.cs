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
    public class BudgetController: Controller
    {
        private BudgetingContext context;
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
                .Include(b => b.IncomeItems);
            return View("Index", budgets);
        }

        // HTTP Get Request
        // A breakdown of a user's budget
        public async Task<IActionResult> BudgetBreakdown(long id)
        {
            Budget budget = await context.Budgets
                .FirstAsync(b => b.BudgetId == id);

            IQueryable<ExpenseCategory> expenseCategories =  context.ExpenseCategories
                .Include(ec => ec.ExpenseItems)
                .Where(ec => ec.BudgetId == id);

            IQueryable<IncomeCategory> incomeCategories =  context.IncomeCategories
                .Include(ic => ic.IncomeItems)
                .Where(ic => ic.BudgetId == id);
            
            BudgetBreakdownViewModel budgetBreakdownViewModel = new BudgetBreakdownViewModel
            {
                Budget = budget,
                ExpenseCategories = expenseCategories,
                IncomeCategories = incomeCategories
            };
            
            return View("BudgetBreakdown", budgetBreakdownViewModel);
        }
    }
}

