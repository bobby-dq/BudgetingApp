using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetingApp.Models.RepositoryModels;
using BudgetingApp.Models.BudgetingModels;

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
                .Include(b => b.ExpenseCategories)
                .Include(b => b.IncomeCategories)
                .Include(b => b.ExpenseItems)
                .Include(b => b.IncomeItems);
            
            return View("Index", budgets);
        }
    }
}